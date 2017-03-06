using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionRetriever : MonoBehaviour {


    #region Singleton
    private static QuestionRetriever instance;

    private QuestionRetriever() { }

    public static QuestionRetriever Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuestionRetriever();
            }
            return instance;
        }
    }
    #endregion

    public void SetupQuestions(GraphManager gm, LessonType lessonType)
    {
        switch (lessonType)
        {
            case LessonType.Intro:
                SetupIntroQuestions(gm);
                break;
            case LessonType.Frog:
                SetupFrogQuestions(gm);
                break;
            case LessonType.Fish:
                SetupFishQuestions(gm);
                break;
            case LessonType.Bower:
                SetupBowerQuestions(gm);
                break;
            case LessonType.Duck:
                SetupDuckQuestions(gm);
                break;
            case LessonType.Caribou:
                SetupCaribouQuestions(gm);
                break;
            case LessonType.Post:
                SetupFinalQuestions(gm);
                break;
        }
    }

    private void SetupIntroQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Select which side of the <i>r</i>-<i>K</i> range the following traits are most associated with";

        Slot s1 = new Slot();
        s1.SetQuestion("Large body size", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s1.SetAns(1, "<i>r</i>", false);
        s1.SetAns(2, "<i>K</i>", true);

        Slot s2 = new Slot();
        s2.SetQuestion("High number of offspring", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s2.SetAns(1, "<i>r</i>", true);
        s2.SetAns(2, "<i>K</i>", false);

        Slot s3 = new Slot();
        s3.SetQuestion("High degree of parental care", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s3.SetAns(1, "<i>r</i>", false);
        s3.SetAns(2, "<i>K</i>", true);

        Slot s4 = new Slot();
        s4.SetQuestion("High infant mortality rate", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s4.SetAns(1, "<i>r</i>", true);
        s4.SetAns(2, "<i>K</i>", false);

        Slot s5 = new Slot();
        s5.SetQuestion("Low dependence on parents", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s5.SetAns(1, "<i>r</i>", true);
        s5.SetAns(2, "<i>K</i>", false);

        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
        gm.AddSlot(s5);
    }

    private void SetupFrogQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Toad Quiz";

        Slot s1 = new Slot();
        s1.SetQuestion("Does the spadefoot toad tend more towards the <i>r</i> or <i>K</i> extreme of the behavioral range?", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this species");
        s1.SetAns(1, "<i>r</i>", true);
        s1.SetAns(2, "<i>K</i>", false);

        Slot s2 = new Slot();
        s2.SetQuestion("Does the spadefoot toad have a high or low mortality rate?", "<i>Mortality rate</i> refers the likelihood that a member of a species will perish.");
        s2.SetAns(1, "High", true);
        s2.SetAns(2, "Low", false);

        Slot s3 = new Slot();
        s3.SetQuestion("Do spadefoot toads take care of their children?", "Do spadefoot toads actively protect or nourish their young?");
        s3.SetAns(1, "Yes", false);
        s3.SetAns(2, "No", true);

        Slot s5 = new Slot();
        s5.SetQuestion("Is the spadefoot toad an example of an opportunistic species?", "<i>Opportunistic species</i> only reproduce when the right environmental conditions arise");
        s5.SetAns(1, "Yes", true);
        s5.SetAns(2, "No", false);

        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s5);
    }

    private void SetupFishQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Salmon Quiz";

        Slot s1 = new Slot();
        s1.SetQuestion("Does the pacific salmon tend more towards the <i>r</i> or <i>K</i> extreme of the behavioral range?", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this species");
        s1.SetAns(1, "<i>r</i>", true);
        s1.SetAns(2, "<i>K</i>", false);

        Slot s2 = new Slot();
        s2.SetQuestion("Do pacific salmon carefully choose their partners?", "Do salmon participate in selective mating?");
        s2.SetAns(1, "Yes", false);
        s2.SetAns(2, "No", true);

        Slot s3 = new Slot();
        s3.SetQuestion("Do pacific salmon perform mating dances?", "Do salmon dance to impress other salmon?");
        s3.SetAns(1, "Yes", false);
        s3.SetAns(2, "No", true);

        Slot s4 = new Slot();
        s4.SetQuestion("Do pacific salmon take care of their offspring?", "Do salmon protect or nourish their young?");
        s4.SetAns(1, "Yes", false);
        s4.SetAns(2, "No", true);

        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
    }

    private void SetupBowerQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Bowerbird Quiz";

        Slot s0 = new Slot();
        s0.SetQuestion("Does the bowerbird tend more towards the <i>r</i> or <i>K</i> extreme of the behavioral range?", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this species");
        s0.SetAns(1, "<i>r</i>", false);
        s0.SetAns(2, "<i>K</i>", true);

        Slot s1 = new Slot();
        s1.SetQuestion("Why do some animals carefully select a mate?", "Why are some animals picky when they choose partners?");
        s1.SetAns(1, "They don't", false);
        s1.SetAns(2, "So their offspring will gain desirable traits", true);
        s1.SetAns(3, "So they will gain desirable traits", false);

        Slot s2 = new Slot();
        s2.SetQuestion("What is the favorite color of most bowerbirds?", "Can you remember?");
        s2.SetAns(1, "Red", false);
        s2.SetAns(2, "Blue", true);
        s2.SetAns(3, "Mauve", false);
        s2.SetAns(4, "Brown", false);

        Slot s3 = new Slot();
        s3.SetQuestion("Some male birds perform elaborate dances to attract females. Why do females of these species try to choose the best dancers?", "Why is it beneficial for females to favor better dancers for mates?");
        s3.SetAns(1, "They tend to be good at protecting eggs", false);
        s3.SetAns(2, "They tend to be good at frightening predators", false);
        s3.SetAns(3, "They tend to know the best sources of food", false);
        s3.SetAns(4, "They tend to pass on favorable traits to their young", true);

        gm.AddSlot(s0);
        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
    }

    private void SetupDuckQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Wood Duck Quiz";

        Slot s0 = new Slot();
        s0.SetQuestion("Does the wood duck tend more towards the <i>r</i> or <i>K</i> extreme of the behavioral range?", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this species");
        s0.SetAns(1, "<i>r</i>", false);
        s0.SetAns(2, "<i>K</i>", false);
        s0.SetAns(3, "Somewhere in the middle", true);

        Slot s1 = new Slot();
        s1.SetQuestion("How does a mother wood duck protect her eggs?", "Which technique do wood ducks use to keep their eggs safe?");
        s1.SetAns(1, "By hiding them in a lake", false);
        s1.SetAns(2, "By hiding them in a nest high up in a tree", true);
        s1.SetAns(3, "By herding them away from predators", false);

        Slot s2 = new Slot();
        s2.SetQuestion("How does a mother wood duck help her ducklings after they have hatched?", "What behavior does a female wood duck engage in to help her offspring reach the water?");
        s2.SetAns(1, "With a courtship dance", false);
        s2.SetAns(2, "With a mating call", false);
        s2.SetAns(3, "By guiding them", true);
        s2.SetAns(4, "By fighting off predators", false);

        Slot s3 = new Slot();
        s3.SetQuestion("What is a normal amount of eggs for a wood duck to lay in one clutch?", "Hint: you had an average amount of ducklings at the beginning of the game");
        s3.SetAns(1, "Fewer than three", false);
        s3.SetAns(2, "Around ten", true);
        s3.SetAns(3, "About as many as a spadefoot toad", false);

        Slot s4 = new Slot();
        s4.SetQuestion("How much parental care do female wood ducks invest in their offspring?", "Compare the amount of parental care given by female wood ducks to the amount given by an <i>r</i> species, like salmon, and the amount given by a <i>K</i> species, like humans");
        s4.SetAns(1, "Little", false);
        s4.SetAns(2, "Somewhere in the middle", true);
        s4.SetAns(3, "A lot", false);

        Slot s5 = new Slot();
        s5.SetQuestion("Why do wood ducks build nests high up in trees? ", "What is the main benefit of treetop nesting for wood ducks?");
        s5.SetAns(1, "To protect their eggs from predators", true);
        s5.SetAns(2, "To attract mates", false);
        s5.SetAns(3, "To give their hatchlings a nice view", false);

        gm.AddSlot(s0);
        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s4);
        gm.AddSlot(s5);
    }

    private void SetupCaribouQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Caribou Quiz";

        Slot s0 = new Slot();
        s0.SetQuestion("Does the barren-ground caribou tend more towards the <i>r</i> or <i>K</i> extreme of the behavioral range?", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this species");
        s0.SetAns(1, "<i>r</i>", false);
        s0.SetAns(2, "Somewhere in the middle", false);
        s0.SetAns(3, "<i>K</i>", true);

        Slot s1 = new Slot();
        s1.SetQuestion("What is the most common amount of calves for a female caribou to give birth to anually?", "How many offspring does a caribou normally have each mating season?");
        s1.SetAns(1, "One", true);
        s1.SetAns(2, "Five", false);
        s1.SetAns(3, "Fifty", false);

        Slot s2 = new Slot();
        s2.SetQuestion("True or false: caribou calves are protected primarily by herds.", "Hint: would the caribou game have been harder or easier if there had been no other caribou besides you and your calf?");
        s2.SetAns(1, "True", true);
        s2.SetAns(2, "False", false);

        Slot s4 = new Slot();
        s4.SetQuestion("Why do barren-ground caribou migrate great distances each year?", "What motivates barren-ground caribou to spend so much energy walking back and forth between the tundra and the taiga?");
        s4.SetAns(1, "To search for other herds of caribou", false);
        s4.SetAns(2, "Because they are always running from predators", false);
        s4.SetAns(3, "Because of seasonal changes to the environment", true);
        s4.SetAns(4, "Because they are bored", false);


        gm.AddSlot(s0);
        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s4);
    }

    private void SetupFinalQuestions(GraphManager gm)
    {
        gm.gameObject.SetActive(true);
        gm.titleText.text = "Final Quiz";
        

        Slot s1 = new Slot();
        s1.SetQuestion("Select which side of the <i>r</i>-<i>K</i> range the trait of <i>producing many offspring</i> is most associated with", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s1.SetAns(1, "<i>r</i>", true);
        s1.SetAns(2, "<i>K</i>", false);

        Slot s2 = new Slot();
        s2.SetQuestion("Select which side of the <i>r</i>-<i>K</i> range the trait of <i>slow maturation</i> is most associated with", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s2.SetAns(1, "<i>r</i>", false);
        s2.SetAns(2, "<i>K</i>", true);

        Slot s3 = new Slot();
        s3.SetQuestion("Select which side of the <i>r</i>-<i>K</i> range the trait of <i>high degree of parental care</i> is most associated with", "Choose whichever side of the <i>r</i>-<i>K</i> range of reproductive behavior you think best represents this trait");
        s3.SetAns(1, "<i>r</i>", false);
        s3.SetAns(2, "<i>K</i>", true);

        Slot s5 = new Slot();
        s5.SetQuestion("What is the main reproductive drawback of providing little or no parental care?", "Do animals that do not provide any care for their young tend to have higher or lower mortality rates?");
        s5.SetAns(1, "Lower mortality rates", false);
        s5.SetAns(2, "Higher mortality rates", true);


        Slot s7 = new Slot();
        s7.SetQuestion("Which of the following species is an opportunistic breeder?", "Which species mates only under specific and irregular environmental conditions?");
        s7.SetAns(1, "Spadefoot toad", true);
        s7.SetAns(2, "Wood duck", false);
        s7.SetAns(3, "Barren-ground caribou", false);


        Slot s8 = new Slot();
        s8.SetQuestion("Nest building is an example of which kind of reproductive strategy?", "What is the main purpose of the nests that some species of animal build?");
        s8.SetAns(1, "Protecting young", true);
        s8.SetAns(2, "Herding behavior", false);
        s8.SetAns(3, "Mating ritual", false);

        gm.AddSlot(s1);
        gm.AddSlot(s2);
        gm.AddSlot(s3);
        gm.AddSlot(s5);
        gm.AddSlot(s7);
        gm.AddSlot(s8);
    }
}
