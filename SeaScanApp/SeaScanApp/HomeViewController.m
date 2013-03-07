//
//  HomeViewController.m
//  SharkScan
//
//  Created by James Masterman on 28/10/12.
//  Copyright 2012 __MyCompanyName__. All rights reserved.
//

#import "HomeViewController.h"

@implementation HomeViewController

@synthesize tableView;

- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil
{
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
       
    }
    return self;
}

- (void)didReceiveMemoryWarning
{
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc that aren't in use.
}


- (void)viewDidLoad
{
   // [super viewDidLoad];
    //
	// Change the properties of the imageView and tableView (these could be set
	// in interface builder instead).
	//
	tableView.separatorStyle = UITableViewCellSeparatorStyleNone;
	tableView.rowHeight = 80;
	tableView.backgroundColor = [UIColor clearColor];
    
    [[SSDataManager getInstance]addRefreshNotifier:^(RefreshTypes type)
    {
        if(type == MISSION_STATS || type == LATEST_TARGETS)
        {
            [tableView reloadData]; //refresh table when new data comes in
        }
        
    }];
	
}

- (void)viewDidUnload
{
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}

- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation
{
    // Return YES for supported orientations
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}


//
// numberOfSectionsInTableView:
//
// Return the number of sections for the table.
//
- (NSInteger)numberOfSectionsInTableView:(UITableView *)tableView
{
	return 1;
}

//
// tableView:numberOfRowsInSection:
//
// Returns the number of rows in a given section.
//
- (NSInteger)tableView:(UITableView *)tableView numberOfRowsInSection:(NSInteger)section
{
	return 4;
}

//
// tableView:cellForRowAtIndexPath:
//
// Returns the cell for a given indexPath.
//
- (UITableViewCell *)tableView:(UITableView *)aTableView cellForRowAtIndexPath:(NSIndexPath *)indexPath
{
	const NSInteger TOP_LABEL_TAG = 1001;
	const NSInteger BOTTOM_LABEL_TAG = 1002;
	UILabel *topLabel;
	UILabel *bottomLabel;

    
	static NSString *CellIdentifier = @"Cell";
	UITableViewCell *cell = [aTableView dequeueReusableCellWithIdentifier:CellIdentifier];
	if (cell == nil)
	{
		//
		// Create the cell.
		//
		cell =
        [[UITableViewCell alloc]
          initWithFrame:CGRectZero
          reuseIdentifier:CellIdentifier];
		
		const CGFloat LABEL_HEIGHT = 20;
		UIImage *image = [UIImage imageNamed:@"TableIconPlane.png"];
        
		//
		// Create the label for the top row of text
		//
		topLabel =
        [[UILabel alloc]
          initWithFrame:
          CGRectMake(
                     image.size.width + 2.0 * cell.indentationWidth,
                     0.5 * (aTableView.rowHeight - 2 * LABEL_HEIGHT),
                     aTableView.bounds.size.width -
                     image.size.width - 4.0 * cell.indentationWidth,
                 //    - indicatorImage.size.width,
                     LABEL_HEIGHT)];
		[cell.contentView addSubview:topLabel];
        
		//
		// Configure the properties for the text that are the same on every row
		//
		topLabel.tag = TOP_LABEL_TAG;
		topLabel.backgroundColor = [UIColor clearColor];
		topLabel.textColor = [UIColor colorWithRed:0.25 green:0.0 blue:0.0 alpha:1.0];
		topLabel.highlightedTextColor = [UIColor colorWithRed:1.0 green:1.0 blue:0.9 alpha:1.0];
		topLabel.font = [UIFont systemFontOfSize:[UIFont labelFontSize]];
        
		//
		// Create the label for the top row of text
		//
		bottomLabel =
        [[UILabel alloc]
          initWithFrame:
          CGRectMake(
                     image.size.width + 2.0 * cell.indentationWidth,
                     0.5 * (aTableView.rowHeight - 2 * LABEL_HEIGHT) + LABEL_HEIGHT,
                     aTableView.bounds.size.width -
                     image.size.width - 4.0 * cell.indentationWidth,
                    // - indicatorImage.size.width,
                     LABEL_HEIGHT)];
		[cell.contentView addSubview:bottomLabel];
        
		//
		// Configure the properties for the text that are the same on every row
		//
		bottomLabel.tag = BOTTOM_LABEL_TAG;
		bottomLabel.backgroundColor = [UIColor clearColor];
		bottomLabel.textColor = [UIColor colorWithRed:0.25 green:0.0 blue:0.0 alpha:1.0];
		bottomLabel.highlightedTextColor = [UIColor colorWithRed:1.0 green:1.0 blue:0.9 alpha:1.0];
		bottomLabel.font = [UIFont systemFontOfSize:[UIFont labelFontSize] - 3];
        
		//
		// Create a background image view.
		//
		cell.backgroundView =
        [[UIImageView alloc] init];
		cell.selectedBackgroundView =
        [[UIImageView alloc] init];

	}
    

	else
	{
		topLabel = (UILabel *)[cell viewWithTag:TOP_LABEL_TAG];
		bottomLabel = (UILabel *)[cell viewWithTag:BOTTOM_LABEL_TAG];
	}
    

	//
	// Set the background and selected background images for the text.
	// Since we will round the corners at the top and bottom of sections, we
	// need to conditionally choose the images based on the row index and the
	// number of rows in the section.
	//
	UIImage *rowBackground;
	UIImage *selectionBackground;
	NSInteger row = [indexPath row];
	
    rowBackground = [UIImage imageNamed:@"TableCellBackground.png"];
    selectionBackground = [UIImage imageNamed:@"TableCellBackground.png"];
    ((UIImageView *)cell.backgroundView).image = rowBackground;
	((UIImageView *)cell.selectedBackgroundView).image = selectionBackground;
	
	//
	// Here I set an image based on the row. This is just to have something
	// colorful to show on each row.
	//
    SSDataManager* dm = [SSDataManager getInstance];
    
	if (row == 0) //missions flown
	{
        NSString* dateString = [SSSettings convertDateToString:[SSSettings earliestDate] formatString:@"E d-MMM"];
        
        SSMissionStats* ms = [dm allMissionStats];
        cell.imageView.image = [UIImage imageNamed:@"TableIconPlane.png"];

        if(ms != nil)
        {
            topLabel.text = [NSString stringWithFormat:@"%d flights over %d beaches", ms.missionCount, ms.beachesCovered];
            bottomLabel.text = [NSString stringWithFormat:@"%.0fkm flown since %@ ", ms.totalDistance, dateString];
        }else
        {
             topLabel.text = @"Nothing scanned";
             bottomLabel.text = @"";
        }
		       

	}
	else 
	{
        NSString* icon = @"TableIconShark.png";
        NSString* spottedDate = @"";
        NSString* spottedLocation = @"";
        
        topLabel.text = @"Nothing scanned";
        bottomLabel.text = @"";

        if([dm allMissionStats] != nil)
        {
            NSArray* lt = [[dm allMissionStats]LatestTargets];
                       
            if(lt != nil && lt.count >= row)
            {
                SSLatestTargets* tgts = lt[row-1];
                icon = getIconFromID([tgts targetTypeID]);
                
                spottedDate = [SSSettings convertDateToString:[tgts dateRecorded] formatString:@"d-MMM H:mma"];
                if(dm.locations != nil && dm.locations.count > 0)
                {
                    SSLocation* locn = [[dm locations]objectForKey:[NSNumber numberWithInteger:[tgts locationID]]];
                    if(locn != nil)
                    {
                        spottedLocation = [locn location];
                    }
                }
                
                topLabel.text = [NSString stringWithFormat:@"Last seen %@",spottedDate];
                bottomLabel.text = [NSString stringWithFormat:@"At %@",spottedLocation];
            }
        }
        
        cell.imageView.image = [UIImage imageNamed:icon];


	}
		
	return cell;
}

NSString* getIconFromID(int tgtID)
{
    NSString* icon =  @"shark64.png";
    if(tgtID == WHALE)
    {
        icon = @"whale64.png";
        
    }else if(tgtID == DOLPHIN)
    {
        icon = @"dolphin64.png";
        
    }else if(tgtID == SEAL)
    {
        icon = @"seal64.png";
    }
    
    return icon;
}

//
// dealloc
//
// Releases instance memory.
//


@end
