-- we don't know how to generate database przproject (class Database) :(
create table KoloNaukowe
(
	Id int identity
		constraint [PK_dbo.KoloNaukowe]
			primary key,
	Nazwa nvarchar(max),
	DataUtworzenia datetime not null,
	Uczelnia int not null,
	Aktywny bit not null,
	DataDoUsuniecia datetime not null
)
go

create table Portfolio
(
	Id int identity
		constraint [PK_dbo.Portfolio]
			primary key,
	IdKola int not null,
	Zawartosc nvarchar(max),
	DataUtworzenia datetime not null,
	KoloNaukowe_Id int
		constraint [FK_dbo.Portfolio_dbo.KoloNaukowe_KoloNaukowe_Id]
			references KoloNaukowe
)
go

create index IX_KoloNaukowe_Id
	on Portfolio (KoloNaukowe_Id)
go

create table Post
(
	Id int identity
		constraint [PK_dbo.Post]
			primary key,
	IdUzytkownika int not null,
	IdKola int not null,
	Zawartosc nvarchar(max),
	DataUtworzenia datetime not null,
	Przypiety bit not null,
	KoloNaukowe_Id int
		constraint [FK_dbo.Post_dbo.KoloNaukowe_KoloNaukowe_Id]
			references KoloNaukowe
)
go

create index IX_KoloNaukowe_Id
	on Post (KoloNaukowe_Id)
go

create table Komentarz
(
	Id int identity
		constraint [PK_dbo.Komentarz]
			primary key,
	IdPostu int not null,
	Zawartosc nvarchar(max),
	DataPrzeslania datetime not null,
	IdUzytkownika int not null,
	Post_Id int
		constraint [FK_dbo.Komentarz_dbo.Post_Post_Id]
			references Post
)
go

create index IX_Post_Id
	on Komentarz (Post_Id)
go

create table Uzytkownik
(
	Id int identity
		constraint [PK_dbo.Uzytkownik]
			primary key,
	Imie nvarchar(max),
	Nazwisko nvarchar(max),
	DataUtworzenia datetime not null,
	DataModyfikacji datetime not null,
	DataLogowania datetime not null,
	Login nvarchar(max),
	Email nvarchar(max),
	Haslo nvarchar(max),
	IDP int not null,
	Salt nvarchar(max),
	Rola int not null
)
go

create table Czlonkowie
(
	Id int identity
		constraint [PK_dbo.Czlonkowie]
			primary key,
	IdKola int not null,
	Rola int not null,
	Aktywny bit not null,
	IdUzytkownika int not null,
	KoloNaukowe_Id int
		constraint [FK_dbo.Czlonkowie_dbo.KoloNaukowe_KoloNaukowe_Id]
			references KoloNaukowe,
	Uzytkownik_Id int
		constraint [FK_dbo.Czlonkowie_dbo.Uzytkownik_Uzytkownik_Id]
			references Uzytkownik
)
go

create index IX_KoloNaukowe_Id
	on Czlonkowie (KoloNaukowe_Id)
go

create index IX_Uzytkownik_Id
	on Czlonkowie (Uzytkownik_Id)
go

create table Event
(
	Id int identity
		constraint [PK_dbo.Event]
			primary key,
	Nazwa nvarchar(max),
	Lokalizacja nvarchar(max),
	DataUtworzenia datetime not null,
	DataRozpoczecia datetime not null,
	DataZakonczenia datetime not null,
	Zawartosc nvarchar(max),
	IdKola int not null
)
go

create table Uczestnictwo
(
	Id int identity
		constraint [PK_dbo.Uczestnictwo]
			primary key,
	IdUzytkownika int not null,
	IdEventu int not null,
	Event_Id int
		constraint [FK_dbo.Uczestnictwo_dbo.Event_Event_Id]
			references Event,
	Uzytkownik_Id int
		constraint [FK_dbo.Uczestnictwo_dbo.Uzytkownik_Uzytkownik_Id]
			references Uzytkownik
)
go

create index IX_Event_Id
	on Uczestnictwo (Event_Id)
go

create index IX_Uzytkownik_Id
	on Uczestnictwo (Uzytkownik_Id)
go

create table Wiadomosc
(
	Id int identity
		constraint [PK_dbo.Wiadomosc]
			primary key,
	IdOdbiorcy int not null,
	IdNadawcy int not null,
	Zawartosc nvarchar(max),
	Tytul nvarchar(max),
	DataPrzeslania datetime not null,
	DataOdebrania datetime not null,
	Nadawca_Id int
		constraint [FK_dbo.Wiadomosc_dbo.Uzytkownik_Nadawca_Id]
			references Uzytkownik,
	Odbiorca_Id int
		constraint [FK_dbo.Wiadomosc_dbo.Uzytkownik_Odbiorca_Id]
			references Uzytkownik,
	Uzytkownik_Id int
		constraint [FK_dbo.Wiadomosc_dbo.Uzytkownik_Uzytkownik_Id]
			references Uzytkownik,
	Uzytkownik_Id1 int
		constraint [FK_dbo.Wiadomosc_dbo.Uzytkownik_Uzytkownik_Id1]
			references Uzytkownik
)
go

create index IX_Nadawca_Id
	on Wiadomosc (Nadawca_Id)
go

create index IX_Odbiorca_Id
	on Wiadomosc (Odbiorca_Id)
go

create index IX_Uzytkownik_Id
	on Wiadomosc (Uzytkownik_Id)
go

create index IX_Uzytkownik_Id1
	on Wiadomosc (Uzytkownik_Id1)
go

create table [__MigrationHistory]
(
	MigrationId nvarchar(150) not null,
	ContextKey nvarchar(300) not null,
	Model varbinary(max) not null,
	ProductVersion nvarchar(32) not null,
	constraint [PK_dbo.__MigrationHistory]
		primary key (MigrationId, ContextKey)
)
go

