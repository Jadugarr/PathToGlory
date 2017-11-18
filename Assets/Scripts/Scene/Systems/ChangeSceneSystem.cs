using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

public class ChangeSceneSystem : ReactiveSystem<GameEntity>, ICleanupSystem
{
    private IGroup<GameEntity> sceneChangeGroup;

    public ChangeSceneSystem(IContext<GameEntity> context) : base(context)
    {
        sceneChangeGroup = context.GetGroup(GameMatcher.ChangeScene);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ChangeScene);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        if (entities.Count > 1)
        {
            throw new ArgumentException("There are too many entites for changing a scene!");
        }
        else
        {
            SceneManager.LoadScene(entities[0].changeScene.SceneName);
        }
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in sceneChangeGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}