use master
go
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'Cronos2000_db')
	Drop database Cronos2000_db
go
create database Cronos2000_db
go
use Cronos2000_db

create table RoleUtil
(
	IdRole int identity(1,1),
	Libelle varchar(38),
	Constraint PK_RoleUtil primary key (IdRole)
)
go
create table Utilisateur
(
	IdUtil int identity(1,1),
	Matricule varchar(15) unique,
	MDP varchar(255),
	Nom varchar(38),
	Prenom varchar(38),
	IdRole int,
	Constraint PK_Utilisateur primary key (IdUtil),
	Constraint FK_Utilisateur_IdRole foreign key (IdRole)
	References RoleUtil(IdRole)
)
go
create table Pointage
(
	IdPointage int identity(1,1),
	IdUtil int,
	dateHeureArriver datetime,
	dateHeureSortie datetime null,
	Constraint PK_Pointage primary key (IdPointage),
	Constraint FK_Pointage_IdUtil foreign key (IdUtil)
	References Utilisateur(IdUtil)
)
go

create proc GetPersonnes 
as
begin	
	select IdUtil,Matricule,Nom,Prenom,Utilisateur.IdRole as IdRole,RoleUtil.Libelle as LibelleRole from Utilisateur inner join RoleUtil on Utilisateur.IdRole=RoleUtil.IdRole
end
go

create proc GetRoles 
as
begin	
	select IdRole,Libelle from RoleUtil
end
go

create proc GetPersonne @IdUtil int
as  
begin	
	select IdUtil,Matricule,Nom,Prenom,Utilisateur.IdRole,RoleUtil.Libelle as LibelleRole from Utilisateur inner join RoleUtil on Utilisateur.IdRole=RoleUtil.IdRole where @IdUtil = IdUtil
end
go

create proc GetPersonneMatricule @Matricule varchar(15)
as  
begin	
	select IdUtil,Matricule,Nom,Prenom,MDP,IdRole from Utilisateur where @Matricule = Matricule
end
go

create proc CreatePersonne @Nom varchar(38),@Prenom varchar(38),@Matricule varchar(15),@MDP varchar(255),@IdRole int
as
begin
	insert into Utilisateur (Nom,Prenom,Matricule,MDP,IdRole)
	values(@Nom,@Prenom,@Matricule,@MDP,@IdRole)
end
go

create proc UpdatePersonne @Id int,@Nom varchar(38),@Prenom varchar(38),@Matricule varchar(15),@IdRole int
as
begin
	update Utilisateur
	set Nom = @Nom,
	Prenom = @Prenom,
	Matricule = @Matricule,
	IdRole = @IdRole
	where IdUtil = @Id
end
go

create proc UpdatePersonneMDP @Id int,@MDP varchar(255)
as
begin
	update Utilisateur
	set MDP = @MDP
	where IdUtil = @Id
end
go


create proc DeletePersonne @Id int
as
begin
	delete Utilisateur where IdUtil = @Id
end
go

create proc GetPointagesUtil @IdUtil int
as
begin
	select TOP 5 IdPointage, IdUtil, dateHeureArriver, dateHeureSortie from pointage where @IdUtil = IdUtil ORDER BY dateHeureArriver DESC
end
go

create proc GetPointageOuvertUtil @IdUtil int
as
begin
	select IdPointage, IdUtil, dateHeureArriver, dateHeureSortie from pointage where @IdUtil = IdUtil AND dateHeureSortie is null
end
go

create proc CreatePointage @IdUtil int,@Arrivee datetime
as
begin
	insert into Pointage (IdUtil, dateHeureArriver, dateHeureSortie)
	values(@IdUtil, @Arrivee, null)
end
go

create proc UpdatePointage @Id int,@Sortie datetime
as
begin
	update Pointage
	set dateHeureSortie = @Sortie
	where IdUtil = @Id
end
go

insert into RoleUtil(Libelle)
values('Administation')

insert into RoleUtil(Libelle)
values('Employee')

insert into RoleUtil(Libelle)
values('Visiteur')

exec CreatePersonne @Nom = 'Follet', @Prenom = 'Yaroslav', @Matricule = 'Admin', @MDP = 'c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d', @IdRole = 1
exec CreatePersonne @Nom = 'Delattre', @Prenom = 'Louis', @Matricule = 'UserTest', @MDP = '89B1360879DDAE764E1261AD23837C8033399E68BF795B6E939EB2166BC59E', @IdRole = 2
exec GetPersonneMatricule @Matricule = 'Admin'
exec GetPersonnes


SELECT * FROM Pointage