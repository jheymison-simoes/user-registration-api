using System.ComponentModel;

namespace UserRegister.Business.Enums;

public enum EnvironmentBuildEnum
{
    [Description("Docker")] Docker = 100,
    [Description("Development")] Development = 200
}