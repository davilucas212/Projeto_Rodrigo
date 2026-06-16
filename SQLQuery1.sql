CREATE TABLE salas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    andar INT NOT NULL,
    quantidadeassentos INT NOT NULL
);

CREATE TABLE reservas (
    id INT IDENTITY(1,1) PRIMARY KEY,
    inicio DATETIME NOT NULL,
    fim DATETIME NOT NULL,
    salaid INT NOT NULL,

    CONSTRAINT FK_Reserva_Sala
        FOREIGN KEY (salaid)
        REFERENCES salas(id)
);