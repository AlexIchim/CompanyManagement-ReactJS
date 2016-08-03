using System;
using Manager.InfoModels;
using Manager.InputInfoModels;

namespace Contracts
{
    public interface IProjectValidator
    {
        bool ValidateId(int id);

        bool ValidateMemberInfo(MemberInfo inputInfo);

        bool ValidateProjectInfo(ProjectInfo inputInfo);

        bool ValidateAddProjectInfo(AddProjectInputInfo addInfo);

        bool ValidateUpdateProjectInfo(UpdateProjectInputInfo updateInfo);
    }
}
