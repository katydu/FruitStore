USE FruitStore
GO

--�s�W�|�����

DECLARE @MemberName NVARCHAR(20)
DECLARE @MemberEmail NVARCHAR(50)
DECLARE @MemberAddress NVARCHAR(70)
DECLARE @MemberPhone NVARCHAR(24)

SET @MemberName = '�p��'
SET @MemberEmail = 'S12344@gmail.com'
SET @MemberAddress = '��饫����Ϥ��D�@��22��'
SET @MemberPhone = '0988888888'

BEGIN TRY
	BEGIN TRAN
		INSERT INTO Members(MemberName, MemberEmail, MemberAddress, MemberPhone) 
			VALUES(@MemberName, @MemberEmail, @MemberAddress, @MemberPhone)
	COMMIT TRAN
END TRY
BEGIN CATCH
	SELECT ERROR_NUMBER() AS ERR_NUM, ERROR_MESSAGE() AS ERR_MSG
	ROLLBACK TRAN
END CATCH;

SELECT *
FROM Members
ORDER BY MemberID

--���ե�:�R�����諸���
DELETE FROM Members WHERE MemberID = '4'
