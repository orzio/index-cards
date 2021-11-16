use quizowanie;
insert into Categories values('Backend',null);
insert into Categories values('C#','1');
insert into Categories values('Async Programming','1');
insert into Categories values('LINQ','1');
insert into Categories values('Abstraction','1');

insert into Categories values('Frontend',null);
insert into Categories values('Angular','6');
insert into Categories values('ReactiveForms','6');
insert into Categories values('Binding','6');
insert into Categories values('Syntax','6');


insert into Categories values('HTML',null);
insert into Categories values('Block Elements','11');
insert into Categories values('Inline Elements','11');

insert into Categories values('CSS',null);
insert into Categories values('FlexBox','14');
insert into Categories values('Streams','7');

select * from Categories

--delete from Categories


--after delete we need to reseed identities because the numbering will start from number one plus previous id
DELETE FROM Categories
DBCC CHECKIDENT ('quizowanie.dbo.Categories',RESEED, 0)


USE quizowanie;  
GO  
ALTER TABLE dbo.Questions   
DROP CONSTRAINT FK_Questions_Categories_CategoryId;

GO  
truncate table categories;

drop database quizowanie;