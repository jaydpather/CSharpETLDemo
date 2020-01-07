select * 
from CustomerBasic

select * from CustomerCompany

begin tran
	update CustomerBasic
	set [Name] = 'Test import 3'
	where CustomerNumber = 'CN12343567'

	update CustomerCompany
		set CustomerNumber = 'CN12343567'
	where CustomerNumber = 'abc123    '
rollback tran
commit tran