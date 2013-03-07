//
//  HomeViewController.h
//  SharkScan
//
//  Created by James Masterman on 28/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import <UIKit/UIKit.h>
#import "SSDataManager.h"
#import "SSMissionStats.h"
#import "SSLatestTargets.h"
#import "SSTargetType.h"

@interface HomeViewController : UIViewController
{
    UITableView *tableView;
}


@property (nonatomic, strong) IBOutlet UITableView *tableView;

NSString* getIconFromID(int tgtID);
@end