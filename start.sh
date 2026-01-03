#!/bin/bash

echo "🚀 Starting Online Learning Platform..."
echo ""

# Start backend
echo "📦 Starting .NET Backend..."
cd "$(dirname "$0")"
snap run dotnet-sdk.dotnet run &
BACKEND_PID=$!

# Wait for backend to be ready
echo "⏳ Waiting for backend to start..."
sleep 5

# Start frontend
echo "🎨 Starting Angular Frontend..."
cd frontend
npm start &
FRONTEND_PID=$!

echo ""
echo "✅ Application is starting!"
echo ""
echo "📍 Backend API: http://localhost:5000"
echo "📍 Frontend: http://localhost:4200"
echo "📍 Swagger API Docs: http://localhost:5000/swagger"
echo ""
echo "Press Ctrl+C to stop both servers"
echo ""

# Wait for Ctrl+C
trap "kill $BACKEND_PID $FRONTEND_PID; exit" INT
wait
