//
//  SharkScanAppDelegate.h
//  SharkScan
//
//  Created by James Masterman on 25/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import <RestKit/RestKit.h>

#import <UIKit/UIKit.h>

@interface SharkScanAppDelegate : NSObject <UIApplicationDelegate, UITabBarControllerDelegate>

@property (nonatomic, strong) IBOutlet UIWindow *window;

@property (nonatomic, strong) IBOutlet UITabBarController *tabBarController;

@end
