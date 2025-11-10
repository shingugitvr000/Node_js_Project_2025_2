const express = require('express');
const mysql = require('mysql2/promise');
const app = express();

app.use(express.json());

const pool = mysql.createPool({
    host : 'localhost',
    user : 'root',
    password : '112233',
    database : 'gametest'

});

//플레이어 로그인
app.post('/login', async (req , res) => {
    const {username, password_hash} = req.body;

    try
    {
        const [players] = await pool.query(
        'SELECT * FROM players WHERE username = ? AND password_hash = ?',
        [username , password_hash]
        );

        if(players.length > 0)
        {
            await pool.query(
              'UPDATE players SET last_login = CURRENT_TIMESTAMP WHERE player_id = ?'
              [players[0].player_id]  
            );
        }
        else
        {
            res.status(500).json({success: false, message : '로그인 실패'});
        }
    }
    catch (error)
    {
        res.status(500).json({success: false, message : error.message});
    }

});

//플레이어 인벤토리 조회 
app.get('/inventory/:playerId', async (req, res) => {

    try
    {
        const[inventory] = await pool.query (
            'SELECT inv.item_id,inv.quantity FROM inventories inv JOIN items i ON inv.item_id = i.item_id WHERE inv.player_id = ?',
            [req.params.playerId]
        );
        res.json(inventory);
    }
    catch(error)
    {
        res.status(500).json({success: false, message : error.message});
    }

});

//퀘스트 목록 조회 
app.get('/quests/:playerId' , async (req,res) => {

    console.log(req.params.playerId);

    try
    {
        console.log("try 문 진입");
        const[quests] = await pool.query (
            'SELECT q.* , pq.`STATUS`FROM player_quests pq JOIN quests q ON pq.quest_id = q.quest_id WHERE pq.player_id = ?',
            [req.params.playerId]
        );

        console.log(quests);
        res.json(quests);
    }
    catch(error)
    {
        res.status(500).json({success: false, message : error.message});
    }

})


const PORT = 3000;

app.listen(PORT , ()=> {
    console.log(`서버 실행 중 : ${PORT}`);
} )