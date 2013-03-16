//
//  SSMission.h
//  SharkScan
//
//  Created by James Masterman on 4/11/12.
//
//

#import <Foundation/Foundation.h>
#import <RestKit/CoreData.h>
#import "SSMissionRoutePoint.h"
#import "SSLocation.h"

@interface SSMission : NSObject
{

}

@property (nonatomic) int missionID;
@property (nonatomic) int locationID;
@property (nonatomic) NSDate*    dateFlown;
@property (nonatomic) double duration;
@property (nonatomic) double distanceFlown;
@property (nonatomic) int numTargetsSpotted;
@property (nonatomic) int pointCount;
@property (nonatomic, copy) NSString* comments;
@property (nonatomic, copy) NSMutableArray* MissionPoints;


-(id) initWithMission:(SSLocation*)l dateFlown:(NSDate*)d targetsSpotted:(NSInteger)ts route:(NSMutableArray*)r comments:(NSString*)c missionID:(UInt32) mid distance:(Float32)dist;

+(RKObjectMapping*) getObjectMapping;
+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath;


@end
