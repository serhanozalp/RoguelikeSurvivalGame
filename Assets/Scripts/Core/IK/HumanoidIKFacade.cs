using Cysharp.Threading.Tasks;
using RootMotion.FinalIK;
using System.Collections.Generic;
using UnityEngine;

public class HumanoidIKFacade
{
    private class IKLerpData
    {
        public float CurrentWeight;
        public float TargetWeight;
        public float LerpDuration;
    }

    private class ChainTargetsData
    {
        public Transform EndEffectorTarget;
        public Transform BendTarget;
    }

    private FullBodyBipedIK _fullBodyBipedIK;
    private Dictionary<FullBodyBipedChain, IKLerpData> _chainLerpDataDictionary;
    private Dictionary<FullBodyBipedChain, ChainTargetsData> _chainTargetsDataDictionary;

    public HumanoidIKFacade(FullBodyBipedIK fullBodyBipedIK)
    {
        _fullBodyBipedIK = fullBodyBipedIK;
        _chainLerpDataDictionary = new Dictionary<FullBodyBipedChain, IKLerpData>();
        _chainTargetsDataDictionary = new Dictionary<FullBodyBipedChain, ChainTargetsData>();
    }

    public virtual void UpdateHook()
    {
        LerpIKWeights();
        UpdateChainTargets();
    }

    private void UpdateChainTargets()
    {
        foreach (var kvp in _chainTargetsDataDictionary)
        {
            ChainTargetsData chainTargetsData = kvp.Value;
            if(chainTargetsData.BendTarget != null) _fullBodyBipedIK.solver.GetChain(kvp.Key).bendConstraint.bendGoal.SetPositionAndRotation(chainTargetsData.BendTarget.position, chainTargetsData.BendTarget.rotation);
            _fullBodyBipedIK.solver.GetEndEffector(kvp.Key).target.SetPositionAndRotation(chainTargetsData.EndEffectorTarget.position, chainTargetsData.EndEffectorTarget.rotation);
        }
    }

    private void LerpIKWeights()
    {
        foreach(var kvp in _chainLerpDataDictionary)
        {
            IKLerpData chainLerpData = kvp.Value;
            if (chainLerpData.CurrentWeight != chainLerpData.TargetWeight)
            {
                chainLerpData.CurrentWeight = Mathf.MoveTowards(chainLerpData.CurrentWeight, chainLerpData.TargetWeight, (1f / chainLerpData.LerpDuration) * Time.deltaTime);
                ApplyWeight(kvp.Key, chainLerpData.CurrentWeight);
            }
        }
    }

    private void ApplyWeight(FullBodyBipedChain chainType, float weight)
    {
        _fullBodyBipedIK.solver.GetEndEffector(chainType).positionWeight = weight;
        _fullBodyBipedIK.solver.GetEndEffector(chainType).rotationWeight = weight;
        _fullBodyBipedIK.solver.GetChain(chainType).bendConstraint.weight = weight;
    }

    public async UniTask MoveLimbToAndFollow(FullBodyBipedChain chainType, Transform target, float moveDuration, Transform bendTarget = null)
    {
        ModifyOrAddChainLerpData(chainType, 1, moveDuration);
        ModifyOrAddChainTargetsData(chainType, target, bendTarget);
        await UniTask.WaitUntil(() => _chainLerpDataDictionary[chainType].CurrentWeight == _chainLerpDataDictionary[chainType].TargetWeight);
    }

    private void ModifyOrAddChainLerpData(FullBodyBipedChain chainType, float targetWeight, float lerpDuration)
    {
        if (_chainLerpDataDictionary.ContainsKey(chainType))
        {
            _chainLerpDataDictionary[chainType].TargetWeight = targetWeight;
            _chainLerpDataDictionary[chainType].LerpDuration = lerpDuration;
        }
        else _chainLerpDataDictionary.Add(chainType, new IKLerpData { TargetWeight = targetWeight, LerpDuration = lerpDuration });
    }

    private void ModifyOrAddChainTargetsData(FullBodyBipedChain chainType, Transform endEffectorTarget, Transform bendTarget)
    {
        if (_chainTargetsDataDictionary.ContainsKey(chainType))
        {
            _chainTargetsDataDictionary[chainType].EndEffectorTarget = endEffectorTarget;
            _chainTargetsDataDictionary[chainType].BendTarget = bendTarget;
        }
        else _chainTargetsDataDictionary.Add(chainType, new ChainTargetsData { EndEffectorTarget = endEffectorTarget, BendTarget = bendTarget });
    }

    protected void ResetChain(FullBodyBipedChain chainType)
    {
        _chainLerpDataDictionary.Remove(chainType);
        _chainTargetsDataDictionary.Remove(chainType);
        ApplyWeight(chainType, 0);
    }
}
