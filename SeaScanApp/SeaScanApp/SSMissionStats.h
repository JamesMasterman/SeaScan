//
//  SSMissionStats.h
//  SharkScan
//
//  Created by James Masterman on 10/02/13.
//
//

#import <Foundation/Foundation.h>
#import <RestKit/RestKit.h>
#import <RestKit/CoreData.h>
#import "SSLatestTargets.h"

@interface SSMissionStats : NSObject
{
    
}

@property (nonatomic) int missionCount;
@property (nonatomic)         double totalDistance;
@property (nonatomic)         int beachesCovered;
@property (nonatomic, strong)  NSArray* LatestTargets;

+(RKObjectMapping*) getObjectMapping;
+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath;

@end
