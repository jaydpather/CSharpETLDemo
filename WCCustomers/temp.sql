create table #input(
	CustomerNumber nvarchar(10) not null,
	CompanyCode nvarchar(4) not null,
	[Name] nvarchar(50) not null,
	IsActive bit null
)

declare @size int = 2

insert into #input values
	('abc1', 'PVHE', 'test1', 1),
	('abc2', 'PVHE', 'test2', null),
	('abc3', 'PVHE', 'test3', 1)

--select top(@size) * from #input

insert into customers(CustomerNumber, CompanyCode, [Name], IsActive)
	(select CustomerNumber, CompanyCode, [Name], IsActive from #input)

drop table #input

select * from Customers c where c.CustomerNumber like 'abc%'