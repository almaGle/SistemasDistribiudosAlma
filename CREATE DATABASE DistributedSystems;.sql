CREATE DATABASE DistributedSystems;



USE DistributedSystems;
-- auto-generated definition
create table dbo.Users
(
    Id        uniqueidentifier not null
        constraint Users_pk
            primary key,
    FirstName varchar(255) collate SQL_Latin1_General_CP1_CI_AS,
    LastName  varchar(255) collate SQL_Latin1_General_CP1_CI_AS,
    Email     varchar(255) collate SQL_Latin1_General_CP1_CI_AS,
    Birthday  datetime2
)
go
