dev:
	docker compose up -d
	dotnet watch run
	
reset-db:
	docker compose down -v
	docker compose up -d
	dotnet ef migrations add InitialMigration --project Infrastructure --startup-project Web
	dotnet ef database update --project Infrastructure --startup-project Web
