using Celeste.DeckBuilding.Persistence;
using System;
using System.Collections.Generic;

namespace WOTR.BoardGame.Persistence
{
    [Serializable]
    public class WOTRPlayerRecordDTO
    {
        public int guid;
        public List<CardRuntimeDTO> cardsInHand = new List<CardRuntimeDTO>();
        public List<CardRuntimeDTO> cardsOnStage = new List<CardRuntimeDTO>();

        public WOTRPlayerRecordDTO(WOTRPlayerRecord wotrPlayerRecord)
        {
            guid = wotrPlayerRecord.Guid;

            {
                cardsInHand.Capacity = wotrPlayerRecord.NumCardsInHand;

                for (int i = 0, n = wotrPlayerRecord.NumCardsInHand; i < n; ++i)
                {
                    cardsInHand.Add(new CardRuntimeDTO(wotrPlayerRecord.GetCardFromHand(i)));
                }
            }

            {
                cardsOnStage.Capacity = wotrPlayerRecord.NumCardsOnStage;

                for (int i = 0, n = wotrPlayerRecord.NumCardsOnStage; i < n; ++i)
                {
                    cardsOnStage.Add(new CardRuntimeDTO(wotrPlayerRecord.GetCardFromStage(i)));
                }
            }
        }
    }
}
