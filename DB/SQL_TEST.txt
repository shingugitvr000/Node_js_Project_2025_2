-- 1. 데이터 베이스 생성
CREATE DATABASE `GameTest` /*!40100 COLLATE 'utf8mb4_0900_ai_ci' */;

-- 2. 테이블 생성
CREATE TABLE `players` (
	`player_id` INT NOT NULL AUTO_INCREMENT,
	`username` VARCHAR(50) NULL DEFAULT NULL,
	`email` VARCHAR(50) NULL DEFAULT NULL,
	`password_hash` VARCHAR(255) NULL DEFAULT NULL,
	`create_at` TIMESTAMP NULL DEFAULT NULL,
	`last_login` TIMESTAMP NULL DEFAULT NULL,
	PRIMARY KEY (`player_id`),
	UNIQUE INDEX `username` (`username`),
	UNIQUE INDEX `email` (`email`)
)

-- 3. 플레이어 데이터 삽입
INSERT INTO players(username , email, password_hash) VALUES 
('hero423', 'hero423@gmail.com' , 'hashed_password4'),
('hero523', 'hero523@gmail.com' , 'hashed_password5'),
('hero623', 'hero623@gmail.com' , 'hashed_password6'),
('hero723', 'hero723@gmail.com' , 'hashed_password7')

-- 4.플레이어 데이터 조회 
SELECT * FROM players
SELECT username, last_login FROM players

-- 5.특정 플레이어 정보 업데이트
UPDATE players SET last_login = CURRENT_TIMESTAMP WHERE username = 'hero123'

-- 6. 조건에 맞는 플레이어 검색 
SELECT username, email FROM players WHERE username LIKE '%hero%'

-- 7. 플레이어 삭제
DELETE FROM players WHERE username = 'hero123'

-- 8. 플레이어 테이블에 새 열 추가
ALTER TABLE players ADD COLUMN `level` INT DEFAULT 1

-- 9. 모든 플레이어의 레벨을 1 증가
UPDATE players SET `level` = `level` + 1

-- 10. 가장 Level이 높은 플레이어가져오기 
SELECT username, `level` FROM players ORDER BY `level` DESC LIMIT 1
