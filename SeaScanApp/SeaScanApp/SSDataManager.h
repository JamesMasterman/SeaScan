//
//  SSDataManager.h
//  SharkScan
//
//  Created by James Masterman on 11/02/13.
//
//

#import <Foundation/Foundation.h>
#import "SSMissionStats.h"
#import "SSLatestTargets.h"
#import "SSWebServiceConsumer.h"
#import "SSMission.h"
#import "SSMissionRoutePoint.h"
#import "SSMissionRoutePointView.h"
#import "SSMissionView.h"
#import "SSTargetType.h"

typedef NS_ENUM(NSInteger, RefreshTypes) {
    LOCATIONS,
    MISSION_STATS,
    LATEST_TARGETS,
    MISSIONS,
    TARGET_TYPES
};



typedef void(^_refreshNotifier)(RefreshTypes objectRefreshed);
@interface SSDataManager : NSObject
{
    SSWebServiceConsumer* dataQuery;
    NSMutableArray* refreshNotifiers;
}

@property (nonatomic, retain) SSMissionStats* allMissionStats;
@property (nonatomic, retain) NSMutableDictionary* missionList;
@property (nonatomic, retain) NSMutableDictionary* locations;
@property (nonatomic, retain) NSMutableDictionary* targetTypes;


+(SSDataManager*) getInstance;

-(NSString*) baseURL;

-(void) addRefreshNotifier:(_refreshNotifier)notifier;


-(void) refreshBaseData; //Refresh locations, target types, mission stats - basically everything but missions

-(int) refreshMissions:(int)locID earliestDate:(NSDate*)dt onlyTargetLocations:(BOOL)targetFound;

-(int) refreshLocations:(NSString*)locID onlyFlownLocations:(BOOL)inUse onlyTargetLocations:(BOOL)targetFound; 

-(int) refreshTargetTypes;
-(int) refreshMissionStats:(NSDate*)dt;

-(SSLocation*) getLocationByID:(int)locID;



@end
