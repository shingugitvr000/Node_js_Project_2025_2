const WebSocket = require('ws');
const iconv = require('iconv-lite');

class GameServer {

    constructor(port){
        this.wss = new WebSocket.Server({port});
        this.clients = new Set();
        this.players = new Map();
        this.SetupServerEvent();
        console.log(`게임 서버 포트 ${port}에서 시작 되었습니다. `);
    }

    SetupServerEvent()
    {
        this.wss.on('connection' , (socket) => {
            this.clients.add(socket);
            const playerId = this.generatePlayerId();

            this.players.set(playerId, {
                socket : socket,
                position: { x:0, y:0, z:0}
            });
            console.log(`클라이언트 접속! ID : ${playerId}, 현재 접속자 : ${this.clients.size}`);

            const welcomData = {
                type : 'connection',
                playerId : playerId,
                message : '서버에 연결 되었습니다!'
            };

            socket.send(JSON.stringify(welcomData));

            socket.on('message' , (message) =>
            {
                try
                {
                    const data = JSON.parse(message);
                    console.log('수신된 메세지  :' , data);

                    //채팅 메세지 브로드캐스트(보내 사람 정보 포함)
                    this.broadcast({
                        type: 'chat',
                        playerId: playerId,
                        message: data.message
                    });

                }
                catch
                {
                    console.error('메세지 파싱 에러 : ' , error);
                }
            });

            socket.on('close', ()=> {
                this.clients.delete(socket);
                this.players.delete(playerId);

                this.broadcast({
                    type : 'playerDisconnect',
                    playerId : playerId
                });
                
                console.log(`클라이언트 퇴장 ID : ${playerId}, 현재 접속자 : ${this.clients.size}`);

            });

            socket.on('error' , (error) => {
                console.error('소켓 에러 : ' , error);
            });
        });
    }

    broadcast(data)
    {
        const message = JSON.stringify(data);
        this.clients.forEach(client => 
        {
            if(client.readyState === WebSocket.OPEN) //===는 비교전에 암시적인 형 변환을 하지 않음 (값이나 타입 중 하나라도 다르면 false 반환)
            {
                client.send(message);
            }            
        });
    }

    generatePlayerId()
    {
        return 'player_' + Math.random().toString(36).substr(2,9);
    }

}


const gameServer = new GameServer(3000);