//
//  Constants.h
//  SharkScan
//
//  Created by James Masterman on 8/11/12.
//
//


#import <Foundation/Foundation.h>


@interface SSSettings

+(UIColor*) targetDetectedPathColour;
+(UIColor*) targetNotDetectedPathColour;

+(int) selectedLocationID;
+(void) setSelectedLocationID:(int)newVar;


+(NSDate*) earliestDate;
+(void) setEarliestDate:(NSDate*) newDate;

+(NSString*) convertDateToString:(NSDate*) dt formatString:(NSString*)dateString;

+(BOOL) onlyShowMissionsWithTarget;
+(void) setOnlyShowMissionsWithTarget:(BOOL)show;

+(BOOL) showTracklines;
+(void) setShowTracklines:(BOOL)show;




    


@end


