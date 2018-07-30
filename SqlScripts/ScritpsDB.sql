USE master;
GO

IF EXISTS (SELECT database_id FROM sys.databases WHERE name = 'DBPedidosNet')
BEGIN
	EXEC msdb.dbo.sp_delete_database_backuphistory @database_name = N'DBPedidosNet'
	ALTER DATABASE DBPedidosNet SET  SINGLE_USER WITH ROLLBACK IMMEDIATE
	DROP DATABASE DBPedidosNet
END
GO

CREATE DATABASE DBPedidosNet
GO

USE DBPedidosNet;
GO

--======= Cliente =================================================================
IF EXISTS(SELECT object_id FROM sys.tables WHERE name = 'Cliente')
BEGIN
	DROP TABLE dbo.Cliente;
END
GO

CREATE TABLE dbo.Cliente(
	IdCliente INT IDENTITY(1,1),
	Nome      VARCHAR(200) NOT NULL,
	Cpf       VARCHAR(11) NOT NULL,
	Telefone  VARCHAR(20) NOT NULL,
	Senha     VARCHAR(50) NOT NULL
)
ALTER TABLE dbo.Cliente ADD CONSTRAINT PK_Cliente PRIMARY KEY (IdCliente)


--======= Produto =================================================================
IF EXISTS(SELECT object_id FROM sys.tables WHERE name = 'Produto')
BEGIN
	DROP TABLE dbo.Produto;
END
GO

CREATE TABLE dbo.Produto(
	IdProduto         INT IDENTITY(1,1) NOT NULL,
	Descricao         VARCHAR(200)  NOT NULL,
	Marca             VARCHAR(200)  NULL,
	ValorUnitario     DECIMAL(10,4) NOT NULL,
	QuantidadeEstoque INT NOT NULL
)
GO

ALTER TABLE dbo.Produto ADD CONSTRAINT PK_Produto PRIMARY KEY (IdProduto)


--======= Pedido ==================================================================
IF EXISTS(SELECT object_id FROM sys.tables WHERE name = 'Pedido')
BEGIN
	DROP TABLE dbo.Pedido;
END
GO

CREATE TABLE dbo.Pedido(
	IdPedido   INT IDENTITY(1,1) NOT NULL,
	IdCliente  INT NOT NULL,
	DataPedido DATETIME NOT NULL,
	Efetuado   BIT NOT NULL
)
GO
ALTER TABLE dbo.Pedido ADD CONSTRAINT PK_Pedido PRIMARY KEY (IdPedido)
ALTER TABLE dbo.Pedido ADD CONSTRAINT FK_Pedido_Cliente FOREIGN KEY (IdCliente) REFERENCES Cliente(IdCliente)


--======= PedidoProduto =============================================================
IF EXISTS(SELECT object_id FROM sys.tables WHERE name = 'PedidoProduto')
BEGIN
	DROP TABLE dbo.PedidoProduto;
END
GO

CREATE TABLE dbo.PedidoProduto(
	IdPedidoProduto INT IDENTITY(1,1) NOT NULL,
	IdPedido        INT NOT NULL,
	IdProduto       INT NOT NULL,
    Quantidade      INT NOT NULL,
	ValorUnitario   DECIMAL(10,4) NOT NULL
)
GO

ALTER TABLE dbo.PedidoProduto ADD CONSTRAINT PK_PedidoProduto PRIMARY KEY (IdPedidoProduto)
ALTER TABLE dbo.PedidoProduto ADD CONSTRAINT FK_PedidoProduto_Pedido FOREIGN KEY (IdPedido) REFERENCES dbo.Pedido(IdPedido)
ALTER TABLE dbo.PedidoProduto ADD CONSTRAINT FK_PedidoProduto_Produto FOREIGN KEY (IdProduto) REFERENCES dbo.Produto(IdProduto)


--======= Insert ===================================================================
/*
INSERT INTO Cliente VALUES('Renato Peixoto','74583719100', '65981150185', '123465');
INSERT INTO Cliente VALUES('Angelo Vieira','65984369826', '74583719100', '123465');
INSERT INTO Cliente VALUES('Bruno de Arruda','65996027982', '74583719100', '123465');

INSERT INTO Produto VALUES('Smartphone Galaxy S9', 'Samsung',4200, 200);
INSERT INTO Produto VALUES('Smartphone Moto Z3 Play','Motorola',1999, 150);
INSERT INTO Produto VALUES('Mouse Logitech','Logitech',90, 350);
INSERT INTO Produto VALUES('Smartphone Zenfone 5','Asus',2900, 100);

INSERT INTO Pedido VALUES(1, GETDATE(),1);
INSERT INTO Pedido VALUES(2, GETDATE(),1);
INSERT INTO Pedido VALUES(3, GETDATE(),1);

INSERT INTO PedidoProduto VALUES(1, 1, 1, 4200);
INSERT INTO PedidoProduto VALUES(1, 3, 1, 90);
INSERT INTO PedidoProduto VALUES(2, 2, 1, 1999);
INSERT INTO PedidoProduto VALUES(3, 4, 1, 2900);
*/