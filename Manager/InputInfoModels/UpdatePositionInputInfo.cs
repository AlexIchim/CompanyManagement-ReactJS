namespace Manager.InputInfoModels
{
    public class UpdatePositionInputInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int InputInfoValidationResult()
        {
            if (Name == null)
            {
                return 1; //Empty position name
            }
            else
                if (Name.Length > 100)
            {
                return 2; //Position name too long
            }
            return 0;
        }
    }
}
