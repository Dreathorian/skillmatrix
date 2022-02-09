// See https://aka.ms/new-console-template for more information

using SkillMatrixAPI.Models;

Console.WriteLine("Hello, World!");
var userController  = new UserController();
//var adminController = new AdminController();
int userId     = userController.CreateUser("null", "null");
Log(userId);

int englishId  = userController.CreateLanguage("english");
Log(englishId);

Log(userController.AddLanguage(userId, englishId, 2));
Log(userController.UpdateLanguage(userId, englishId, 3));

int programmingId = userController.CreateSkillCategory("Programming");
int cSharpId = userController.CreateSkill("C#", programmingId);
Log(programmingId);
Log(cSharpId);

Log(userController.AddSkill(userId, cSharpId, 2));
Log(userController.UpdateSkill(userId, cSharpId, 1));

void Log(object item)
{
    Console.WriteLine(item);
}