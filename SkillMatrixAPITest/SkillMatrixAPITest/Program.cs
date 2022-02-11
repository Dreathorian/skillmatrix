// See https://aka.ms/new-console-template for more information

using SkillMatrixAPI.Models;
using SkillMatrixAPITest.Controllers;
using SkillMatrixAPITest.DI;
using SkillMatrixAPITest.DTOs;
using SkillMatrixAPITest.Repositories;

SimpleDI.RegisterSingleton<IDatabase, Database>();
SimpleDI.RegisterSingleton<IUserRepository, UserRepository>();
SimpleDI.RegisterSingleton<IAdminRepository, AdminRepository>();

var adminController = new AdminController();
var userController  = new UserController();

var userId = adminController.CreateUser(new CreateUserDTO("hi", "nope"));
Log(userId);

int englishId  = adminController.CreateLanguage("english");
Log(englishId);

Log(userController.AddLanguage(userId, englishId, 2));
Log(userController.UpdateLanguage(userId, englishId, 3));

int programmingId = adminController.CreateSkillCategory("Programming");
int cSharpId = adminController.CreateSkill("C#", programmingId);

Log(programmingId);
Log(cSharpId);

Log(userController.AddSkill(userId, cSharpId, 2));
Log(userController.UpdateSkill(userId, cSharpId, 1));

void Log(object item)
{
    Console.WriteLine(item);
}