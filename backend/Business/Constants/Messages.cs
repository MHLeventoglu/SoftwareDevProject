using System;
using Core.Entities.Concrete;

namespace Business.Constants;

public static class Messages
{
    public const string ProductAdded = "Product added successfully.";
    public const string ProductUpdated = "Product updated successfully.";
    public const string ProductDeleted = "Product deleted successfully.";
    public const string ProductNotFound = "Product not found.";
    public const string ProductListed = "Products listed successfully.";
    public const string ProductNameInvalid = "Product name is invalid.";
    public const string ProductPriceInvalid = "Product price is invalid.";

    public const string UserNotFound = "User not found.";
    public const string PasswordError = "Incorrect password.";
    public const string UserRegistered = "User registered successfully.";
    public const string SuccessfulLogin = "Login successful.";
    public const string UserExists = "User already exists.";
    public const string AccessTokenCreated = "Access token created successfully.";

    // Add more messages here as needed...
}
