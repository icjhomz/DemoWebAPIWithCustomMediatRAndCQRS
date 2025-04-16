# Custom Mediator with CQRS Pattern

A lightweight, dependency-free implementation of the **Mediator Pattern** following the **CQRS (Command Query Responsibility Segregation)** principle.

---

## ✨ Why This Implementation?

### ✅ **Simplicity**

A custom implementation designed specifically for your application's needs — no fluff, no overengineering.

### 🚫 **No External Dependencies**

No need to install third-party packages like `MediatR`. Keep your project lean and focused.

### 🎯 **Full Control**

You own the code. You understand every part of it. Debugging and extending it is straightforward.

### 📄 **Licensing Freedom**

No need to worry about restrictive commercial licenses or open-source compliance.

---

## 📦 Features

- Clear separation between **Commands** and **Queries**
- Easy to register and resolve handlers
- Designed with testability and extensibility in mind

---

## 🔧 Getting Started

### Folder Structure

```
/Application
  /Commands
  /Queries
  /Common
    - IMediator.cs
    - ICommandHandler.cs
    - IQueryHandler.cs
    - Mediator.cs
```

### Sample Usage

```csharp
// Command
public record CreateUserCommand(string Name, string Email) : ICommand<Guid>;

// Handler
public class CreateUserHandler : ICommandHandler<CreateUserCommand, Guid>
{
    public Task<Guid> Handle(CreateUserCommand command)
    {
        // Handle logic
        return Task.FromResult(Guid.NewGuid());
    }
}

// Execution
var mediator = new Mediator(...); // Register handlers manually or with DI
var result = await mediator.Send(new CreateUserCommand("John Doe", "john@example.com"));
```

---

## 🧪 Testing

Easily mock and test individual handlers without relying on external packages or container setups.

---

## 📌 Note

This implementation is intentionally minimal and can be adapted to:

- Use reflection for handler discovery
- Integrate with dependency injection frameworks
- Add pipeline behaviors or decorators

---

## 📁 License

This project is yours. No license constraints. Use it freely in commercial or open-source software.

