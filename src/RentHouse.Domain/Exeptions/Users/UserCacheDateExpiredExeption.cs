﻿namespace RentHouse.Domain.Exeptions.Users;

public class UserCacheDateExpiredExeption : ExpiredExeption
{
    public UserCacheDateExpiredExeption()
    {
        TitleMessage = "User data has expired!";
    }
}
