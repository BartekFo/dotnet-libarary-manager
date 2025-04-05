# Library Manager

## Environment Setup

1. Copy `.env.example` to `.env`:
```bash
cp .env.example .env
```

2. Update the `.env` file with your secure database credentials:
```env
DB_PASSWORD=your_secure_password_here
DB_USER=sa
DB_NAME=BlazorAppDb
DB_SERVER=localhost
```

3. Start the application:
```bash
docker-compose up -d
```

## Security Notes

- Never commit the `.env` file to version control
- Always use strong passwords in production
- Keep your environment variables secure and don't share them
- Use different passwords for development and production environments 