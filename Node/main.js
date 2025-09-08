let express = require('express');                   //express 모듈을 가져온다.
let app = express();                                //express를 App 이름으로 정의하고 사용한다.

app.get('/' , function(req, res){                   //기본 라우터에서 Hello world 를 반환한다. 
    res.send('Hello world');
});

app.get('/about', function(req, res){               ///about에서 about data 를 반환한다. 
    res.send('Player data 11111 ');
});

app.listen(3000, function(){

    console.log('listening on port 3000');         // 3000포트에서 입력을 대기 한다. 
});