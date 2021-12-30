handlers.GetUserLocation = function (args, context) {
 
    var request = {
        "PlayFabId": currentPlayerId,
        "InfoRequestParameters": {
            "GetPlayerProfile": true,
            "ProfileConstraints":
            {
                "ShowLocations": true
            }
        }
    };
 
    var getUserInfo = server.GetPlayerCombinedInfo(request);
    var userLocation = getUserInfo.InfoResultPayload.PlayerProfile.Locations;
    return  { userLocation };
};