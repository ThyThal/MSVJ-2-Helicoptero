using Helicopter_Components;
using Helicopter_Controllers;
using UnityEngine;
using UnityEditor;


namespace Menu_Editor
{
    public class CreateMenu_Editor
    {
        [MenuItem("Create/Vehicles/Setup New Helicopter")]
        public static void BuildNewHelicopter()
        {
            //Create a new Helicopter Setup
            GameObject currenHelicopter = new GameObject("New_Helicopter", typeof(Helicopter_Main_Controller));

            //Create the COG object for the Helicopter
            GameObject curCOG = new GameObject("COG");
            curCOG.transform.SetParent(currenHelicopter.transform);

            //Assign the COG to the curHeli
            Helicopter_Main_Controller curController = currenHelicopter.GetComponent<Helicopter_Main_Controller>();
            curController.centerOfGravity = curCOG.transform;

            //Create Groups
            GameObject audioGRP = new GameObject("Audio_GRP");
            GameObject graphicsGRP = new GameObject("Graphics_GRP");
            GameObject colGRP = new GameObject("Collision_GRP");
            GameObject engineGRP = new GameObject("Engine_GRP");
            SetupEngineGRP(engineGRP, curController);
            GameObject rotorGRP = new GameObject("Rotor_GRP");
            SetupRotorGRP(rotorGRP, curController);

            audioGRP.transform.SetParent(currenHelicopter.transform);
            graphicsGRP.transform.SetParent(currenHelicopter.transform);
            colGRP.transform.SetParent(currenHelicopter.transform);
            engineGRP.transform.SetParent(currenHelicopter.transform);
            rotorGRP.transform.SetParent(currenHelicopter.transform);

            //Select new helicopter
            Selection.activeGameObject = currenHelicopter;
        }

        static void SetupRotorGRP(GameObject rotorgo, Helicopter_Main_Controller controller)
        {
            Helicopter_Rotors_Controller rotorController = rotorgo.AddComponent<Helicopter_Rotors_Controller>();
            controller.rotorsController = rotorController;

            GameObject mainGRP = new GameObject("Main_Rotor");
            mainGRP.AddComponent<Helicopter_MainRotor>();
            GameObject tailGRP = new GameObject("Tail_Rotor");
            tailGRP.AddComponent<Helicopter_TailRotor>();

            mainGRP.transform.SetParent(rotorgo.transform);
            tailGRP.transform.SetParent(rotorgo.transform);
        }

        static void SetupEngineGRP(GameObject enginego, Helicopter_Main_Controller controller)
        {
            GameObject engineGRP = new GameObject("Main_Engine");
            Helicopter_Engine engine = engineGRP.AddComponent<Helicopter_Engine>();
            controller.engines.Add(engine);

            engineGRP.transform.SetParent(enginego.transform);
        }
    }
}
