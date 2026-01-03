#!/bin/bash

# Online Learning Platform - Setup Script
# This script automates the initial setup process

echo "=========================================="
echo "Online Learning Platform - Setup Script"
echo "=========================================="
echo ""

# Check if .NET 7 SDK is installed
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET SDK not found. Please install .NET 7 SDK first."
    echo "   Download from: https://dotnet.microsoft.com/download/dotnet/7.0"
    exit 1
fi

echo "✅ .NET SDK found: $(dotnet --version)"
echo ""

# Restore NuGet packages
echo "📦 Restoring NuGet packages..."
dotnet restore
if [ $? -ne 0 ]; then
    echo "❌ Failed to restore packages"
    exit 1
fi
echo "✅ Packages restored successfully"
echo ""

# Check if EF Core tools are installed
if ! dotnet ef --version &> /dev/null; then
    echo "📥 Installing Entity Framework Core tools..."
    dotnet tool install --global dotnet-ef
    if [ $? -ne 0 ]; then
        echo "❌ Failed to install EF Core tools"
        exit 1
    fi
    echo "✅ EF Core tools installed"
else
    echo "✅ EF Core tools found: $(dotnet ef --version)"
fi
echo ""

# Build the project
echo "🔨 Building project..."
dotnet build
if [ $? -ne 0 ]; then
    echo "❌ Build failed"
    exit 1
fi
echo "✅ Build successful"
echo ""

# Ask user if they want to create the database
read -p "🗄️  Do you want to create the database now? (y/n): " create_db

if [ "$create_db" = "y" ] || [ "$create_db" = "Y" ]; then
    echo ""
    echo "📋 Creating database migrations..."
    
    # Check if migrations already exist
    if [ -d "Data/Migrations" ] && [ "$(ls -A Data/Migrations 2>/dev/null)" ]; then
        echo "⚠️  Migrations folder already exists"
        read -p "   Do you want to remove existing migrations and create new ones? (y/n): " remove_migrations
        
        if [ "$remove_migrations" = "y" ] || [ "$remove_migrations" = "Y" ]; then
            rm -rf Data/Migrations
            echo "   Removed existing migrations"
        fi
    fi
    
    # Create migration if it doesn't exist
    if [ ! -d "Data/Migrations" ] || [ ! "$(ls -A Data/Migrations 2>/dev/null)" ]; then
        dotnet ef migrations add InitialCreate
        if [ $? -ne 0 ]; then
            echo "❌ Failed to create migration"
            exit 1
        fi
        echo "✅ Migration created"
    fi
    
    echo ""
    echo "🔄 Applying migrations to database..."
    dotnet ef database update
    if [ $? -ne 0 ]; then
        echo "❌ Failed to update database"
        echo "   Please check your connection string in appsettings.json"
        exit 1
    fi
    echo "✅ Database created and seeded successfully"
fi

echo ""
echo "=========================================="
echo "✅ Setup completed successfully!"
echo "=========================================="
echo ""
echo "Next steps:"
echo "1. Review configuration in appsettings.json"
echo "2. Run the application: dotnet run"
echo "3. Open Swagger UI: https://localhost:5001"
echo "4. Check QUICKSTART.md for testing instructions"
echo ""
echo "For detailed documentation, see README.md"
echo ""
