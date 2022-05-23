use ClinicManagement
go

create table Users
(
UserID int identity(1,1),
UserName varchar(20),
FirstName varchar(20),
LastName varchar(20),
P@ssword varchar(10)
)
select*from Users

insert into Users(UserName,FirstName,LastName,P@ssword)values('Admin','S','Haris','1234')


select*from Users where UserName = 'admin' and P@ssword ='1234'

create proc ChkUsr
@Username varchar(20),
@Password varchar(10)
as
select*from Users where UserName like @Username and P@ssword=@Password




create table Doctor
(
DoctorID int identity(1,1),
FirstName varchar(10),
LastName varchar(10),
Sex varchar(8),
Specializations varchar(30),
VisitingHours varchar(20)
)
select*from Doctor
select * from Doctor where DoctorID=23
delete from Doctor where DoctorID=18
delete from Doctor where DoctorID=1
--create Doctor procedure
create proc AddDoctor
@fname varchar(10),
@lname varchar(10),
@sex varchar(8),
@spec varchar(30),
@visithrs varchar(20)
as
insert into Doctor(FirstName,LastName,Sex,Specializations,VisitingHours)
values(@fname,@lname,@sex,@spec,@visithrs)

--Showing Doctor list

create proc ShowDoc
as
select DoctorID,CONCAT(FirstName,LastName)DoctorName,Specializations,VisitingHours
from Doctor ORDER BY DoctorID

create proc PassDoc
@id int
as
select FirstName,Specializations,VisitingHours
from Doctor where DoctorID = @id



create table Patient
(
PatientID int identity(1,1),
FirstName varchar(10),
LastName varchar(10),
Sex varchar(8),
Age int,
DateofBirth Date,
constraint PK_PatientID primary key (PatientID)
)

select *from Patient
delete from Patient where PatientID=3
select DATEDIFF(YEAR,DateofBirth,getdate()) from Patient
--Patient Procedure

 create proc AddPatient
@fname varchar(10),
@lname varchar(10),
@sex varchar(8),
@dob date
as
begin
declare @age int
set @age=(select DATEDIFF(YEAR,@dob,getdate()))
insert into Patient(FirstName,LastName,Sex,Age,DateofBirth)
values(@fname,@lname,@sex,@age,@dob)
end
exec AddPatient 'giri','dharan','Male','2000-06-13'
exec AddPatient 'giri','dharan','Male','2000-06-13'



create table Schedules
(
AppointmentID int identity(1,1),
PatientID int,
PatientName varchar(20),
Specializations varchar(30),
DoctorName varchar(20),
VisitDate date,
AppointmentTime varchar(20),
constraint PK_Apt primary key (AppointmentID),
constraint FK_Patient foreign key (PatientID) references Patient(PatientID)
)

select*from Schedules

--add schedule procedure
create proc Addsch
@id int ,
@pname varchar(20),
@spec varchar(30),
@Dname varchar(20),
@VD date,
@appmntime varchar(20)
as
insert into Schedules(PatientID,PatientName,Specializations,DoctorName,VisitDate,AppointmentTime)
values(@id,@pname,@spec,@Dname,@VD,@appmntime)

exec Addsch 1,'Keerthana','internal medicine','Jenni','2022-05-23','2PM-3PM'

delete Schedules where AppointmentID = 5

--DELETE schedule

create proc Delsch
@id int
as
delete Schedules where AppointmentID = @id


create proc GetAppmnt
as 
select AppointmentID,PatientID,PatientName,Specializations,DoctorName,convert(varchar(10),format(VisitDate,'d','en-IN')) as VisitDate,AppointmentTime 
from Schedules  where VisitDate>=CONVERT(date,GETDATE()) ORDER BY AppointmentID

exec GetAppmnt


exec GetAppmnt

create proc GetPatients
as 
select PatientID,CONCAT(FirstName,LastName)PatientName,Sex,Age,convert(varchar(10),format(DateofBirth,'d','en-IN')) as DateofBirth
from Patient ORDER BY PatientID

create proc Delpat
@id int
as
delete Patient where PatientID = @id

--del by date proc
create proc ShowByDate
@vd varchar(10)
as
select PatientID,DoctorName,Specializations,VisitDate
from Schedules
where VisitDate = @vd

create proc DelDoctor
@id int
as 
delete Doctor where DoctorID = @id

select format(GETDATE(),'d','en-IN')
AS [DATE IN INDIA FORMAT]