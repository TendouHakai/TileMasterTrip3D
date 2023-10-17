using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardMenu : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] RewardItem rewardItemFrefab;

    [SerializeField] List<Reward> rewards = new List<Reward>();
    [SerializeField] List<RewardItem> rewardItems = new List<RewardItem>();

    public void Start()
    {
        
    }

    public void LoadRewards(List<Reward> rewards)
    {
        for(int i = 0; i < rewards.Count; i++)
        {
            this.rewards.Add(rewards[i]);
            RewardItem item = Instantiate(rewardItemFrefab);
            item.ID = rewards[i].IDRewardConfig;
            item.SL = rewards[i].SL;

            item.transform.SetParent(grid.transform); 
            this.rewardItems.Add(item);
        }
    }

    public void OnClaimBtnClick()
    {
        for (int i = 0; i < rewards.Count; i++)
        {
            switch (rewards[i].IDRewardConfig)
            {
                case 0:
                    HUBManger.getInstance().coin += rewards[i].SL;
                    break;
                case 1:
                    //HUBManger.getInstance().Bakc += rewards[i].SL;
                    break;
                case 2:
                    //HUBManger.getInstance().coin += rewards[i].SL;
                    break;
                case 3:
                    //HUBManger.getInstance().coin += rewards[i].SL;
                    break;
                case 4:
                    //HUBManger.getInstance().coin += rewards[i].SL;
                    break;
            }

            Destroy(rewardItems[i].gameObject); 
        }
        rewards.Clear();

        this.gameObject.SetActive(false);
    }
}
