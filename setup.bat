@echo off
REM Online Learning Platform - Setup Script for Windows
REM This script automates the initial setup process

echo ==========================================
echo Online Learning Platform - Setup Script
echo ==========================================
echo.

REM Check if .NET 7 SDK is installed
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo X .NET SDK not found. Please install .NET 7 SDK first.
    echo   Download from: https://dotnet.microsoft.com/download/dotnet/7.0
    pause
    exit /b 1
)

echo √ .NET SDK found
dotnet --version
echo.

REM Restore NuGet packages
echo [*] Restoring NuGet packages...
dotnet restore
if errorlevel 1 (
    echo X Failed to restore packages
    pause
    exit /b 1
)
echo √ Packages restored successfully
echo.

REM Check if EF Core tools are installed
dotnet ef --version >nul 2>&1
if errorlevel 1 (
    echo [*] Installing Entity Framework Core tools...
    dotnet tool install --global dotnet-ef
    if errorlevel 1 (
        echo X Failed to install EF Core tools
        pause
        exit /b 1
    )
    echo √ EF Core tools installed
) else (
    echo √ EF Core tools found
    dotnet ef --version
)
echo.

REM Build the project
echo [*] Building project...
dotnet build
if errorlevel 1 (
    echo X Build failed
    pause
    exit /b 1
)
echo √ Build successful
echo.

REM Ask user if they want to create the database
set /p create_db="Do you want to create the database now? (y/n): "

if /i "%create_db%"=="y" (
    echo.
    echo [*] Creating database migrations...
    
    REM Check if migrations already exist
    if exist "Data\Migrations" (
        echo ! Migrations folder already exists
        set /p remove_migrations="  Do you want to remove existing migrations and create new ones? (y/n): "
        
        if /i "!remove_migrations!"=="y" (
            rmdir /s /q "Data\Migrations"
            echo   Removed existing migrations
        )
    )
    
    REM Create migration if it doesn't exist
    if not exist "Data\Migrations" (
        dotnet ef migrations add InitialCreate
        if errorlevel 1 (
            echo X Failed to create migration
            pause
            exit /b 1
        )
        echo √ Migration created
    )
    
    echo.
    echo [*] Applying migrations to database...
    dotnet ef database update
    if errorlevel 1 (
        echo X Failed to update database
        echo   Please check your connection string in appsettings.json
        pause
        exit /b 1
    )
    echo √ Database created and seeded successfully
)

echo.
echo ==========================================
echo √ Setup completed successfully!
echo ==========================================
echo.
echo Next steps:
echo 1. Review configuration in appsettings.json
echo 2. Run the application: dotnet run
echo 3. Open Swagger UI: https://localhost:5001
echo 4. Check QUICKSTART.md for testing instructions
echo.
echo For detailed documentation, see README.md
echo.
pause
