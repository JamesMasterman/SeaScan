//
//  SSDataQuery.h
//  SharkScan
//
//  Created by James Masterman on 31/10/12.
//
//

#import <Foundation/Foundation.h>
#import "SSLocation.h"

typedef void(^_completionHandler)(int callbackid, NSArray* result);
@interface SSWebServiceConsumer : NSObject
{
    NSMutableDictionary* completionNotifiers;
}

@property(nonatomic, readonly) NSString* baseURL;
@property(nonatomic, readonly) NSString* serviceFile;
@property(nonatomic, readonly) NSString* user;
@property(nonatomic, readonly) NSString* password;


-(int) getMissions:(int)locID earliestDate:(NSDate*)dt onlyTargetLocations:(BOOL)targetFound completionCallback:(_completionHandler)handler;

-(int) getLocations:(NSString*)locID onlyFlownLocations:(BOOL)inUse onlyTargetLocations:(BOOL)targetFound completionCallback:(_completionHandler)handler;

-(int) getTargetTypes:(_completionHandler)handler;
//-(int) getLatestTargets:(_completionHandler)handler;
-(int) getMissionStats:(NSDate*)dt completionCallback:(_completionHandler)handler;

@end
