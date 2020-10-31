using AlkalineThunder.Pandemic;
using AlkalineThunder.Pandemic.Scenes;
using CircleGame.Scenes;

namespace CircleGame
{
    [RequiresModule(typeof(SceneSystem))]
    public class GameModule : EngineModule
    {
        private SceneSystem SceneSystem => GetModule<SceneSystem>();
        protected override void OnInitialize()
        {
            SceneSystem.GoToScene<HelloWorldScene>();
        }
    }
}
