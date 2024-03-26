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
	MDP varchar(50),
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
	dateHeureSortie datetime,
	Constraint PK_Pointage primary key (IdPointage),
	Constraint FK_Pointage_IdUtil foreign key (IdUtil)
	References Utilisateur(IdUtil)
)
go

create proc GetPersonnes 
as
begin	
	select IdUtil,Matricule,Nom,Prenom from Utilisateur
end
go

create proc GetPersonne @IdUtil int
as  
begin	
	select IdUtil,Matricule,Nom,Prenom,IdRole from Utilisateur where @IdUtil = IdUtil
end
go

create proc GetPersonneMatricule @Matricule varchar(15)
as  
begin	
	select IdUtil,Matricule,Nom,Prenom,MDP,IdRole from Utilisateur where @Matricule = Matricule
end
go

create proc CreatePersonne @Nom varchar(38),@Prenom varchar(38),@Matricule varchar(15),@MDP varchar(50),@IdRole int
as
begin
	insert into Utilisateur (Nom,Prenom,Matricule,MDP,IdRole)
	values(@Nom,@Prenom,@Matricule,@MDP,@IdRole)
end
go

create proc UpdatePersonne @Id int,@Nom varchar(38),@Prenom varchar(38),@Matricule varchar(15),@MDP varchar(50),@IdRole int
as
begin
	update Utilisateur
	set Nom = @Nom,
	Prenom = @Prenom,
	Matricule = @Matricule,
	MDP = @MDP,
	IdRole = @IdRole
	where IdUtil = @Id
end
go

create proc DeletePersonne @Id int
as
begin
	delete Utilisateur where IdUtil = @Id
end
go

insert into RoleUtil(Libelle)
values('Administation')

insert into RoleUtil(Libelle)
values('employee')

exec CreatePersonne @Nom = 'Follet', @Prenom = 'Yaroslav', @Matricule = 'Admin', @MDP = 'c1c224b03cd9bc7b6a86d77f5dace40191766c485cd55dc48caf9ac873335d6f', @IdRole = 1

exec GetPersonneMatricule @Matricule = 'Admin'
