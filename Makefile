dev:
	docker compose up -d
	dotnet watch run

create-migration:
	dotnet ef migrations add InitialMigration \
		--project Infrastructure \
		--startup-project Web

migrate:
	dotnet ef database update \
		--project Infrastructure \
		--startup-project Web

seed:
	docker exec -i devpulse_db \
		psql -U devuser -d devpulse_db \
		< database/seed.sql

reset-db:
	docker compose down -v
	docker compose up -d
	sleep 3
	dotnet ef database update \
		--project Infrastructure \
		--startup-project Web
	docker exec -i devpulse_db \
		psql -U devuser -d devpulse_db \
		< database/seed.sql