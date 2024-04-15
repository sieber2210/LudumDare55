public class BasicMelee : Skill
{
    public static BasicMelee Instance; // could act as a default for trash mobs. Bosses may want to have their own instance with higher damage numbers/range.

    static BasicMelee() 
    {
        Instance = new BasicMelee();
    }

    public override int range()
    {
        return 1;
    }

    public override void perform(Character target) 
    {
        // reduce health of target
    }
}
