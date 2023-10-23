# File Sharing Application in ASP.NET

ENG:
Student project of file sharing application made in C# ASP.NET technology

The application allows users to register and log in.
The user can register themselves.
After logging in, the user can upload files, download other users' files and delete files (which he uploaded himself).
Without logging in, you can only see the list of files on the server.

Assumptions about files:
Each user can only upload 4 files. If he wants to upload more, he has to free up space (delete another file).
The application blocks the upload of a file larger than 3.0 KB.
Two files with the same name cannot be uploaded.

Admin has access to the administration panel.
Admin can add user to admin role.

Adding roles:

To be able to add a role, you must be logged in with an administrator account  
The built-in admin account is, login: admin@admin password: Admin_123!

After logging into the admin account, you will see the "Admin Panel" tab, there you can add other users to the Administrator role.
After registering a new user, Admin can add him/her in Admin Panel to admin role.
Once added, this new user will also be able to go to the Admin Panel and add other users to roles.

----------------------------------------------------------------------------------------------------------------------------
PL:
Projekt studencki aplikacji udostępniania plików wykonany w technologii C# ASP.NET.

Aplikacja pozwala na zarejestrowanie i logowanie użytkowników.
Użytkownik może sam sie zarejestrować.
Po zalogowaniu, użytkownik może wgrać pliki, pobrać pliki innych użytkowników oraz usunąc pliki (które sam wgrał).
Bez zalogowania się, można zobaczyć tylko listę plików na serwerze.

Założenia odnośnie plików:
Każdy użytkownik może wgrać tylko 4 pliki. Jeżeli chce wgrać więcej, to musi zwolnić miejsce (usunąć inny plik).
Aplikacja blokuje przesłanie pliku większego niż 3.0 KB.
Nie można przesłać dwóch pliki o takiej samej nazwie.

Dostęp do panelu administracyjnego ma Admin.
Administrator może dodać użytkownika do roli administratora.

Dodawanie ról:

Aby móc dodać rolę, trzeba być zalogowanym na koncie administratora  
Wbudowane konto administratora to, login:  admin@admin hasło: Admin_123!

Po zalogowaniu sie na konto admina, pojawi się zakładka "Panel Administracyjny", tam można dodawać innych użytkowników do roli Administratora.
Po rejestracji nowego użytkownika, Administrator może go dodać w Panelu Administracyjnym do roli adminia.
Po dodaniu, ten nowy użytkownik też będzie mógł wejść na panel administratora i dodawać innym użytkownikom role.

