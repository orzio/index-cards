use quizowanie;
select * from Questions
--insert into Questions values('1','What is that');
insert into Answers values('I dont know','1');

--update Answers set CategoryId =2 where CategoryId =1

delete from Questions where id in(8,9)