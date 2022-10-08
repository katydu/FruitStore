USE FruitStore
GO

--新增會員資料

DECLARE @MemberName NVARCHAR(20)
DECLARE @MemberEmail NVARCHAR(50)
DECLARE @MemberAddress NVARCHAR(70)
DECLARE @MemberPhone NVARCHAR(24)

SET @MemberName = '小五'
SET @MemberEmail = 'S12344@gmail.com'
SET @MemberAddress = '桃園市平鎮區王道一街22號'
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

--測試用:刪除不對的資料
DELETE FROM Members WHERE MemberID = '4'
