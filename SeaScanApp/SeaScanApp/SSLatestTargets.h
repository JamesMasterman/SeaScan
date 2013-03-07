//
//  SSLatestTargets.h
//  SharkScan
//
//  Created by James Masterman on 10/02/13.
//
//

#import <Foundation/Foundation.h>
#import <RestKit/RestKit.h>
#import <RestKit/CoreData.h>

@interface SSLatestTargets : NSObject
{
    
}

@property (nonatomic, assign) int targetTypeID;
@property (nonatomic, assign) int missionID;
@property (nonatomic, retain) NSDate* dateRecorded;
@property (nonatomic, assign) int locationID;

+(RKObjectMapping*) getObjectMapping;
+(RKResponseDescriptor*) getResponseDescriptor:(NSString*)responsePath;

@end
