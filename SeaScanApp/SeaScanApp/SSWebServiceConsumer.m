//
//  SSDataQuery.m
//  SharkScan
//
//  Created by James Masterman on 31/10/12.
//
//

#import "SSWebServiceConsumer.h"
#import "SSSettings.h"
#import "SSMission.h"
#import "SSTargetType.h"
#import "SSLatestTargets.h"
#import "SSMissionStats.h"
#import "Login.h"


@implementation SSWebServiceConsumer

@synthesize baseURL,user,password,serviceFile;


-(id)init
{
    self = [super init];
    serviceFile = @"service.php";
    baseURL =@"http://www.sharkscan.com.au";
    user = SEASCAN_USER;
    password = SEASCAN_PASSWORD;
    
    completionNotifiers = [[NSMutableDictionary alloc]initWithCapacity:10];
        
    return self;
}

-(NSString*) getMySQLStringFromDate:(NSDate*) dt
{
    return [SSSettings convertDateToString:dt formatString:@"yyyy-MM-dd"];
}

-(int) buildWebServiceRequest:(NSString*) resourcePath responseDescriptor:(RKResponseDescriptor*) descriptor completionCallback:(_completionHandler)handler
{
    
    int retCode = -1;
    NSLog(@"%@",resourcePath);
    
    NSURL *URL = [NSURL URLWithString:resourcePath];
    NSURLRequest *request = [NSURLRequest requestWithURL:URL];
    
    NSArray* descriptors = [[NSArray alloc]initWithObjects:descriptor, nil];
    
    RKObjectRequestOperation *objectRequestOperation = [[RKObjectRequestOperation alloc] initWithRequest:request responseDescriptors:descriptors];
    
    retCode = objectRequestOperation.hash;
    
    //keep handler
    [completionNotifiers setObject:handler forKey:[NSNumber numberWithInteger:retCode]];
    
    [objectRequestOperation setCompletionBlockWithSuccess:^(RKObjectRequestOperation *operation, RKMappingResult *mappingResult)
     {
        int hashCode = [operation hash];
         _completionHandler handler = (_completionHandler)[completionNotifiers objectForKey:[NSNumber  numberWithInteger:hashCode]];
         
         handler (hashCode, mappingResult.array);
         [completionNotifiers removeObjectForKey:[NSNumber  numberWithInteger:hashCode]];
         NSLog(@"Loaded result: %@", mappingResult);

         
     } failure:^(RKObjectRequestOperation *operation, NSError *error)
     {
         int hashCode = [operation hash];
         _completionHandler handler = (_completionHandler)[completionNotifiers objectForKey:[NSNumber  numberWithInteger:hashCode]];
         
         handler (hashCode, nil);
         NSLog(@"Operation failed with error: %@", error);
         [completionNotifiers removeObjectForKey:[NSNumber  numberWithInteger:hashCode]];
     }];
    
    [objectRequestOperation start];
    
    return retCode;
}


-(int) getMissions:(int)locID earliestDate:(NSDate*)dt onlyTargetLocations:(BOOL)targetFound
                   completionCallback:(_completionHandler)handler;

{
    int retCode = -1;
    @synchronized(self)
    {
        NSString* dateString = @"all";
        if(dt != nil)
        {
            dateString = [self getMySQLStringFromDate:dt];
        }
        
         NSString* responsePath = [NSString stringWithFormat:@"/%@/%@/%@/%@/%d/%@/%@", serviceFile, @"missions", user, password, locID, dateString ,targetFound?@"true":@"false"];
        
        RKResponseDescriptor* descriptor = [SSMission getResponseDescriptor:responsePath];
        NSString* fullResourcePath = [NSString stringWithFormat:@"%@%@", baseURL, responsePath];
        
        NSLog(@"%@",fullResourcePath);
        
        retCode = [self buildWebServiceRequest:fullResourcePath responseDescriptor:descriptor completionCallback:handler];
        
    }
    
    return retCode;

}


-(int) getLocations:(NSString*)locID onlyFlownLocations:(BOOL)inUse onlyTargetLocations:(BOOL)targetFound completionCallback:(_completionHandler)handler;
{
    int retCode = -1;
    @synchronized(self)
    {
        NSString* responsePath = [NSString stringWithFormat:@"/%@/%@/%@/%@/%@/%@/%@", serviceFile, @"locations", user, password, locID, inUse?@"true":@"false",targetFound?@"true":@"false"];
        
        RKResponseDescriptor* descriptor = [SSLocation getResponseDescriptor:responsePath];
        
        NSString* fullResourcePath = [NSString stringWithFormat:@"%@%@", baseURL, responsePath];
        
        NSLog(@"%@",fullResourcePath);
        
        retCode = [self buildWebServiceRequest:fullResourcePath responseDescriptor:descriptor completionCallback:handler];
    }
    
    return retCode;
}

-(int) getTargetTypes:(_completionHandler)handler
{
    int retCode = -1;
    @synchronized(self)
    {
        NSString* responsePath = [NSString stringWithFormat:@"/%@/%@/%@/%@", serviceFile, @"targettypes", user, password];
        
        RKResponseDescriptor* descriptor = [SSTargetType getResponseDescriptor:responsePath];
        NSString* fullResourcePath = [NSString stringWithFormat:@"%@%@", baseURL, responsePath];
        
        NSLog(@"%@",fullResourcePath);
        
       retCode = [self buildWebServiceRequest:fullResourcePath responseDescriptor:descriptor completionCallback:handler];
    }
    
    return retCode;
}

-(int) getMissionStats:(NSDate*)dt completionCallback:(_completionHandler)handler
{
    int retCode = -1;
    @synchronized(self)
    {
        NSString* dateString = @"all";
        if(dt != nil)
        {
            dateString = [self getMySQLStringFromDate:dt];
        }
        
        NSString* responsePath = [NSString stringWithFormat:@"/%@/%@/%@/%@/%@", serviceFile, @"missions/flightstats", user, password, dateString];
        
        RKResponseDescriptor* descriptor = [SSMissionStats getResponseDescriptor:responsePath];
        NSString* fullResourcePath = [NSString stringWithFormat:@"%@%@", baseURL, responsePath];
        
        NSLog(@"%@",fullResourcePath);
        
        retCode = [self buildWebServiceRequest:fullResourcePath responseDescriptor:descriptor completionCallback:handler];
        
    }
    
    return retCode;
    
}




@end
