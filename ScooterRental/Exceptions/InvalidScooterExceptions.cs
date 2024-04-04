using System;

//Var darit šādi ka visus liek vienā? noteikti ka nē:D
public class InvalidScooterIdException : Exception
{
    public InvalidScooterIdException(string message) : base(message) { }
}

public class InvalidPriceException : Exception
{
    public InvalidPriceException(string message) : base(message) { }
}

public class InvalidDuplicationException : Exception
{
    public InvalidDuplicationException(string message) : base(message) { }
}

public class NoScootersException : Exception
{
    public NoScootersException(string message) : base(message) { }
}

public class InvalidStartRentException : Exception
{
    public InvalidStartRentException(string message) : base(message) { }
}

public class InvalidEndRentException : Exception
{
    public InvalidEndRentException(string message) : base(message) { }
}

public class ScooterIsNullException : Exception
{
   public ScooterIsNullException(string message) : base(message) { }
}

public class ScooterAlreadyExistsException : Exception
{
    public ScooterAlreadyExistsException(string message) : base(message) { }
}

public class RentedScooterIsNullException : Exception
{
    public RentedScooterIsNullException(string message) : base(message) { }
}

public class InvalidRentEndException : Exception
{
   public InvalidRentEndException(string message) : base(message) { }
}

public class InvalidYearException : Exception
{
    public InvalidYearException(string message) : base(message) { }
}

public class InvalidRentDurationException : Exception
{
    public InvalidRentDurationException(string message) : base(message) { }
}