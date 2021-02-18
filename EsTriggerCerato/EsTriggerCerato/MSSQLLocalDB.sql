CREATE TRIGGER StoricoCancellazioni
ON Clienti AFTER DELETE
AS
BEGIN 
  DECLARE @currData DATETIME;
  SET @currData=GETDATE()
  INSERT INTO StoricoCancellazioni(
   idCliente, 
   NomeOld,
   CognomeOld,
   CarrelloOld,
   NomeNew,
   CognomeNew,
   CarrelloNew,
   Data
  )
SELECT d.idCLiente, d.Nome, d.Cognome, d.IdCarrello
FROM deleted d

END

-------------

CREATE TRIGGER StoricoAggiornamenti
ON Cliente AFTER UPDATE
AS
BEGIN
	DECLARE @currData DATETIME;
	SET @currData=GETDATE()
	INSERT INTO StoricoAggiornamenti(
	idCliente, 
	NomeOld,
	CognomeOld,
	CarrelloOld,
	NomeNew,
	CognomeNew,
	CarrelloNew,
	Data
	)
SELECT d.IdCliente, d.Nome, d.Cognome, d.IdCarrello, i.Nome, i.Cognome, i.IdCarrello, @currData
FROM deleted d

END