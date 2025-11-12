-- 1️⃣ Criar o database
CREATE DATABASE "DavinTI";

-- 2️⃣ Conectar ao database
\c "DavinTI";

-- 3️⃣ Criar tabela contato
CREATE TABLE contato (
    id_contato SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    idade INTEGER
);

-- 4️⃣ Criar tabela telefone
CREATE TABLE telefone (
    id SERIAL NOT NULL,
    id_contato INTEGER NOT NULL,
    numero VARCHAR(16) NOT NULL,
    CONSTRAINT pk_telefone PRIMARY KEY (id, id_contato),
    CONSTRAINT fk_telefone_contato FOREIGN KEY (id_contato) 
        REFERENCES contato(id_contato) ON DELETE CASCADE
);

-- 5️⃣ Inserir dados de exemplo na tabela contato
INSERT INTO contato (nome, idade) VALUES
('João', 25),
('Maria', 30),
('Carlos', 22),
('Ana', 28),
('Pedro', 35),
('Lucas', 27),
('Fernanda', 31),
('Ricardo', 29),
('Juliana', 33),
('Marcos', 24),
('Patricia', 26),
('Rafael', 32),
('Aline', 23),
('Bruno', 34),
('Carla', 28),
('Tiago', 27),
('Daniela', 29),
('Felipe', 31),
('Simone', 26),
('Leandro', 33),
('Bianca', 25),
('Eduardo', 32),
('Vanessa', 28),
('Gabriel', 30),
('Sabrina', 27),
('Rodrigo', 34),
('Camila', 29),
('Marcelo', 31),
('Natália', 26),
('Paulo', 35),
('Monique', 28),
('Diego', 27),
('Caroline', 29),
('Igor', 33),
('Julio', 24),
('Larissa', 32);

-- 6️⃣ Inserir dados de telefone
INSERT INTO telefone (id, id_contato, numero) VALUES
(1, 1, '(11) 91234-5678'),
(2, 1, '(11) 99876-5432'),
(3, 2, '(21) 93456-7812'),
(4, 3, '(31) 98765-1234'),
(5, 4, '(19) 91234-0000'),
(6, 5, '(47) 95555-6666'),
(7, 6, '(51) 97777-1111'),
(8, 6, '(51) 93333-4444'),
(9, 7, '(62) 90000-2222'),
(10, 8, '(41) 92345-6789'),
(11, 9, '(85) 98888-1111'),
(12, 9, '(85) 97777-2222'),
(13, 10, '(71) 93456-7890'),
(14, 11, '(11) 98888-9999'),
(15, 12, '(31) 97777-8888'),
(16, 13, '(48) 91212-3434'),
(17, 14, '(83) 95555-2222'),
(18, 15, '(41) 92222-3333'),
(19, 16, '(19) 93333-2222'),
(20, 17, '(61) 91111-0000'),
(21, 18, '(51) 96666-9999'),
(22, 19, '(11) 95555-1111'),
(23, 20, '(31) 92222-8888'),
(24, 21, '(47) 94444-7777'),
(25, 22, '(21) 95555-6666'),
(26, 23, '(81) 91111-5555'),
(27, 24, '(85) 97777-4444'),
(28, 25, '(31) 98888-3333'),
(29, 26, '(41) 93333-2222'),
(30, 27, '(19) 92222-1111'),
(31, 28, '(11) 95555-0000'),
(32, 29, '(62) 96666-7777'),
(33, 30, '(47) 94444-8888'),
(34, 31, '(51) 98888-6666'),
(35, 32, '(21) 97777-3333'),
(36, 33, '(31) 96666-5555'),
(37, 34, '(11) 92222-9999'),
(38, 35, '(41) 95555-4444'),
(39, 35, '(41) 96666-3333');