using Celeste.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WOTR.BoardGame.Map
{
    [Serializable]
    public struct TerritoryConnection
    {
        public Territory territory1;
        public Territory territory2;
    }

    [Serializable]
    public class ExpandedTerritoryConnection
    {
        public Territory territory;
        public List<Territory> connectedTerritories = new List<Territory>();
    }

    [CreateAssetMenu(fileName = nameof(TerritoryTopology), menuName = "WOTR/Board Game/Map/Territory Topology")]
    public class TerritoryTopology : ListScriptableObject<TerritoryConnection>
    {
#if UNITY_EDITOR
        [SerializeField] private List<ExpandedTerritoryConnection> expandedTerritoryConnections = new List<ExpandedTerritoryConnection>();

        public void CollapseExpandedTerritoryConnections()
        {
            List<TerritoryConnection> territoryConnections = new List<TerritoryConnection>();

            foreach (var expandedConnections in expandedTerritoryConnections)
            {
                foreach (var territory in expandedConnections.connectedTerritories)
                {
                    if (!territoryConnections.Exists(x => 
                        (x.territory1 == expandedConnections.territory && x.territory2 == territory) ||
                        (x.territory1 == territory && x.territory2 == expandedConnections.territory)))
                    {
                        territoryConnections.Add(new TerritoryConnection()
                        {
                            territory1 = expandedConnections.territory,
                            territory2 = territory
                        });
                    }
                }
            }

            SetItems(territoryConnections);
        }

        public void ApplyExpandedTerritoryConnectionsSymmetrically()
        {
            List<ExpandedTerritoryConnection> expandedConnectionsToAdd = new List<ExpandedTerritoryConnection>();

            foreach (var expandedConnections in expandedTerritoryConnections)
            {
                foreach (var territory in expandedConnections.connectedTerritories)
                {
                    if (!expandedTerritoryConnections.Exists(x => x.territory == territory))
                    {
                        expandedConnectionsToAdd.Add(new ExpandedTerritoryConnection() { territory = territory });
                    }
                }
            }

            expandedTerritoryConnections.AddRange(expandedConnectionsToAdd);

            foreach (var expandedConnections in expandedTerritoryConnections)
            {
                foreach (var territory in expandedConnections.connectedTerritories)
                {
                    var expandedTerritoryConnection = expandedTerritoryConnections.Find(x => x.territory == territory);

                    if (!expandedTerritoryConnection.connectedTerritories.Contains(expandedConnections.territory))
                    {
                        expandedTerritoryConnection.connectedTerritories.Add(expandedConnections.territory);
                    }
                }
            }

            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
    }
}
