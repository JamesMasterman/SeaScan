//
//  test.m
//  SharkScan
//
//  Created by James Masterman on 9/11/12.
//
//

#import "SSSettings.h"
#import "SSLocation.h"

@implementation SSSettings

+(UIColor*) targetDetectedPathColour{ return [UIColor redColor];}
+(UIColor*) targetNotDetectedPathColour {return [UIColor greenColor];}

static int selectedLocationIndex;
+ (int)selectedLocationID { return selectedLocationIndex; }
+ (void)setSelectedLocationID:(int)newVar { selectedLocationIndex = newVar; }


static NSDate* earliestDate;
+(NSDate*) earliestDate {return earliestDate;}
+(void) setEarliestDate:(NSDate*) newDate
{
    if (earliestDate != newDate) {
        earliestDate = newDate;
    }
}

static BOOL onlyShowMissionsWithTarget;
+(BOOL) onlyShowMissionsWithTarget { return onlyShowMissionsWithTarget;}
+(void) setOnlyShowMissionsWithTarget:(BOOL)show
{
    onlyShowMissionsWithTarget = show;
}

+(NSString*) convertDateToString:(NSDate*) dt formatString:(NSString*)dateString
{
    
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
       // [dateFormatter setDateFormat:@"EEE d-MMM-yyyy"];
    [dateFormatter setDateFormat:dateString];
   // dateFormatter.timeZone = [NSTimeZone timeZoneWithAbbreviation:@"UTC"];

    NSLog([dt description]);
    
    NSString *formattedDateString = [dateFormatter stringFromDate:dt];
    NSLog(@"formattedDateString: %@", formattedDateString);

    
    return formattedDateString;
}

static BOOL showTracklines;
+(BOOL) showTracklines
{
    return onlyShowMissionsWithTarget;
}

+(void) setShowTracklines:(BOOL)show
{
    showTracklines = show;
}

//+(NSString*) convertDateToMySQLString:(NSDate*) dt{
//    
//    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
//    [dateFormatter setDateFormat:@"yyyy-MMM-d"];
//    
//    NSString *formattedDateString = [dateFormatter stringFromDate:dt];
//    NSLog(@"formattedDateString: %@", formattedDateString);
//    
//    return formattedDateString;
//}

@end



