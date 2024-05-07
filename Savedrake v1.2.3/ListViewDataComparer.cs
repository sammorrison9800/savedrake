using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Savedrake_v1._2._3
{
    public class ListViewItemDateComparer : IComparer
    {
        private int col;
        private SortOrder sortOrder;

        public ListViewItemDateComparer(int column, SortOrder order)
        {
            col = column;
            sortOrder = order;
        }

        public int Compare(object x, object y)
        {
            ListViewItem item1 = x as ListViewItem;
            ListViewItem item2 = y as ListViewItem;
            int result;

            if (col == 0) // Name column
            {
                // Compare the text (name) of the items
                result = string.Compare(item1.SubItems[col].Text, item2.SubItems[col].Text);
            }
            else if (col == 1) // Date column
            {
                // Check if the Tag property is not null and is a FileInfo object
                if (item1?.Tag is FileInfo fileInfo1 && item2?.Tag is FileInfo fileInfo2)
                {
                    DateTime date1 = fileInfo1.CreationTime;
                    DateTime date2 = fileInfo2.CreationTime;
                    result = DateTime.Compare(date1, date2);
                }
                else
                {
                    // Handle the case where Tag is null or not a FileInfo object
                    result = 0;
                }
            }
            else
            {
                // Default to 0 if the column index is unknown
                result = 0;
            }

            // Reverse the result if the sort order is descending
            if (sortOrder == SortOrder.Descending)
            {
                result *= -1;
            }

            return result;
        }
    }
}
