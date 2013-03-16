//
//  SSDataManager.m
//  SharkScan
//
//  Created by James Masterman on 11/02/13.
//
//

#import "SSDataManager.h"

@implementation SSDataManager

static SSDataManager* _instance = nil;

@synthesize missionList, allMissionStats, locations, targetTypes;

-(id) init
{

    @synchronized(self)
    {
        dataQuery=nil;
        missionList = nil;
        refreshNotifiers = nil;
        allMissionStats = nil;
        locations = nil;
        targetTypes = nil;
        
        if ( _instance != nil ) {
            [NSException raise:NSInternalInconsistencyException
                        format:@"[%@ %@] cannot be called; use +[%@ %@] instead",
             NSStringFromClass([self class]), NSStringFromSelector(_cmd),
             NSStringFromClass([self class]),
             NSStringFromSelector(@selector(_instance))];
            
        } else if ( self = [super init] ) {
            _instance = self;
            refreshNotifiers = [[NSMutableArray alloc] initWithCapacity:10];
            dataQuery = [[SSWebServiceConsumer alloc]init];
        }
        
        return _instance;
    }

}

+(SSDataManager*) getInstance
{
	@synchronized(self)
	{
        if (!_instance)
            [[SSDataManager alloc] init];
        
        return _instance;
    }
    
	return nil;
}

-(int) refreshMissions:(int)locID earliestDate:(NSDate*)dt onlyTargetLocations:(BOOL)targetFound
{
    int retCode = -1;
    if(dataQuery != nil)
    {

        retCode = [dataQuery getMissions:locID earliestDate:dt onlyTargetLocations:targetFound  completionCallback:^(int callbackID, NSArray* result)
                   {
                       if(result != nil)
                       {
                           if(result.count > 0)
                           {
                               missionList = [[NSMutableDictionary alloc] initWithCapacity:[result count]];
                               
                               for(SSMission* m in result)
                               {
                                   NSMutableArray* ptArray = [[NSMutableArray alloc] initWithCapacity:[result count]];
                                   
                                   for(SSMissionRoutePoint* pt in [m MissionPoints])
                                   {
                                       pt.parentMissionID = m.missionID;
                                       SSMissionRoutePointView* pv = [[SSMissionRoutePointView alloc]initWithPoint:pt];
                                       
                                       
                                       [ptArray addObject:pv];
                                   }
                                   
                                   m.MissionPoints = ptArray;
                                   
                                   SSMissionView* mv = [[SSMissionView alloc]initWithRoute:m];
                                   
                                   [missionList setObject:mv forKey:[NSNumber numberWithInteger:[m missionID]]];
                                   
                               }
                               
                               for(id handler in refreshNotifiers)
                               {
                                   ((_refreshNotifier)handler)(MISSIONS);
                               }
                           }
                       }
                       else
                       {
                           locations = nil;
                           
                       }
                       
                       
                   }];
    }
    
    return retCode;

}

-(int) refreshLocations:(NSString*)locID onlyFlownLocations:(BOOL)inUse onlyTargetLocations:(BOOL)targetFound {
    int retCode = -1;
    if(dataQuery != nil)
    {
        retCode = [dataQuery getLocations:locID onlyFlownLocations:inUse onlyTargetLocations:targetFound completionCallback:^(int callbackID, NSArray* result)
         {
             if(result != nil)
             {
                 if(result.count > 0)
                 {
                     locations = [[NSMutableDictionary alloc] initWithCapacity:[result count]];
                     
                     for(SSLocation* loc in result)
                     {
                         [locations setObject:loc forKey:[NSNumber numberWithInteger:[loc locID]]];
                         if([SSSettings selectedLocationID] ==0 && [[loc location]isEqualToString:ALL_LOCATIONS])
                         {
                             [SSSettings setSelectedLocationID:[loc locID]];
                         }
                     }
                     
                     for(id handler in refreshNotifiers)
                     {
                         ((_refreshNotifier)handler)(LOCATIONS);
                     }
                 }
             }
             else
             {
                 locations = nil;
                
             }
             
             
         }];
    }

    return retCode;
}

-(int) refreshTargetTypes
{
    int retCode = -1;
    if(dataQuery != nil)
    {
        retCode = [dataQuery getTargetTypes:^(int callbackID, NSArray* result)
                   {
                       if(result != nil)
                       {
                           if(result.count > 0)
                           {
                               targetTypes = [[NSMutableDictionary alloc] initWithCapacity:[result count]];
                               
                               for(SSTargetType* tt in result)
                               {
                                   [targetTypes setObject:tt forKey:[NSNumber numberWithInteger:[tt targetTypeID]]];
                               }
                               
                               for(id handler in refreshNotifiers)
                               {
                                   ((_refreshNotifier)handler)(TARGET_TYPES);
                               }
                           }
                       }
                       else
                       {
                           locations = nil;
                           
                       }
                       
                       
                   }];
    }
    
    return retCode;
    
}


-(int) refreshMissionStats:(NSDate*)dt 
{
    int retCode = -1;
    if(dataQuery != nil)
    {
        retCode = [dataQuery getMissionStats:dt completionCallback:^(int callbackID, NSArray* result)
        {
             if(result != nil)
             {
                 if(result.count > 0)
                 {
                     allMissionStats = [result objectAtIndex:0]; //get the first array element - there should be only 1
                     
                     for(id handler in refreshNotifiers)
                     {
                         ((_refreshNotifier)handler)(MISSION_STATS);
                     }
                 }
             }
             else
             {
                 allMissionStats = nil;
                
             }
         }];
    }
    
    return retCode;
}

-(SSLocation*) getLocationByID:(int)locID
{
    SSLocation* loc = nil;
    if(locations!= nil)
    {
        loc = [locations objectForKey:[NSNumber numberWithInteger:locID]];
    }
    
    return loc;
}

-(void) refreshBaseData
{
    //get all locations
    [self refreshLocations:@"all" onlyFlownLocations:FALSE onlyTargetLocations:FALSE];
    
    //get mission stats
    [self refreshMissionStats:[SSSettings earliestDate]];
    
    //get target types
    [self refreshTargetTypes];
}

-(void) addRefreshNotifier:(_refreshNotifier)notifier
{
    [refreshNotifiers addObject:notifier];
}

-(NSString*) baseURL
{
    return [dataQuery baseURL];
}




@end
