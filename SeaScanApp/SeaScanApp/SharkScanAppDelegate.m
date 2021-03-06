//
//  SharkScanAppDelegate.m
//  SharkScan
//
//  Created by James Masterman on 25/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import "SharkScanAppDelegate.h"
#import "SSSettings.h"
#import "SSDataManager.h"

@implementation SharkScanAppDelegate

@synthesize window = _window;
@synthesize tabBarController = _tabBarController;

- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions
{
    // Override point for customization after application launch.
    
    NSDateFormatter *tempFormatter = [[NSDateFormatter alloc]init];
	[tempFormatter setDateFormat:@"yyyy-MMM-d"];
    
    //default to last 1 month of missions to limit data download
    NSCalendar *calendar = [NSCalendar currentCalendar];
    NSDate *today = [NSDate date];
    
    NSDateComponents *lastMonthComponents = [[NSDateComponents alloc] init];
    [lastMonthComponents setMonth:-2];
    NSDate *startDate = [calendar dateByAddingComponents:lastMonthComponents toDate:today options:0];
    
    [SSSettings setEarliestDate:startDate];
    NSLog(@"Earliest date default set to %@",[[SSSettings earliestDate]description]);
    [SSSettings setSelectedLocationID:0];
    [SSSettings setOnlyShowMissionsWithTarget:FALSE];
    
    SSDataManager* dm = [SSDataManager getInstance];
    [dm refreshBaseData];
    
    
    //get a list of all missions newer than given date from webservice
    
    // Add the tab bar controller's current view as a subview of the window
    self.window.rootViewController = self.tabBarController;
    [self.window makeKeyAndVisible];
   
    
    return YES;
}

- (void)applicationWillResignActive:(UIApplication *)application
{
    /*
     Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
     Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
     */
}

- (void)applicationDidEnterBackground:(UIApplication *)application
{
    /*
     Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later. 
     If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
     */
}

- (void)applicationWillEnterForeground:(UIApplication *)application
{
    /*
     Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
     */
}

- (void)applicationDidBecomeActive:(UIApplication *)application
{
    /*
     Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
     */
}

- (void)applicationWillTerminate:(UIApplication *)application
{
    /*
     Called when the application is about to terminate.
     Save data if appropriate.
     See also applicationDidEnterBackground:.
     */
}


/*
// Optional UITabBarControllerDelegate method.
- (void)tabBarController:(UITabBarController *)tabBarController didSelectViewController:(UIViewController *)viewController
{
}
*/

/*
// Optional UITabBarControllerDelegate method.
- (void)tabBarController:(UITabBarController *)tabBarController didEndCustomizingViewControllers:(NSArray *)viewControllers changed:(BOOL)changed
{
}
*/

@end
